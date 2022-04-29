using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.ViewModels.Courses;

namespace Smarty.Pages.Courses
{
    public class RegisterdStudentsModel : PageModel
    {
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly IInstructorService _instructorService;
		private readonly UserManager<SmartyUser> _userManager;

		public RegisterdStudentsModel(IUnitOfWork context,
			IMapper mapper,
			UserManager<SmartyUser> userManager,
			IInstructorService instructorService)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
			_instructorService = instructorService;
		}

		public SelectList SelectList;

		public async Task OnGetAsync(int? selectedCourseId)
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instructorService.GetCoursesSelectListAsync(instructorId, selectedCourseId);

		}
		public async Task<IActionResult> OnGetStudentsAsync(int? courseId)
		{
			if (courseId == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);

			if (course == null)
				return NotFound();

			var studentsCourses = await _context.StudentsCourses.FindByCriteriaAsync(cg => cg.CourseId == courseId , cg=>cg.Student);
			var students = studentsCourses.Select(sc => sc.Student); 

			var registerdStudentsViewModels = _mapper.Map<IEnumerable<RegisterdStudentViewModel>>(students);
			return new JsonResult(registerdStudentsViewModels);
		}

	}
}
