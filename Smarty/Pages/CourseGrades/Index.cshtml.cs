using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.SmartyDBContext;
using Smarty.Data.ViewModels.CourseGrades;

namespace Smarty.Pages.CourseGrades
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly IInstructorService _instructorService;
		private readonly UserManager<SmartyUser> _userManager;

		public IndexModel(IUnitOfWork context,
			IMapper mapper,
			UserManager<SmartyUser> userManager,
			IInstructorService instructorService)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
			_instructorService = instructorService;
		}

		public SelectList  SelectList;

		public async Task OnGetAsync(int? selectedCourseId)
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instructorService.GetCoursesSelectListAsync(instructorId, selectedCourseId);

		}
		public async Task<IActionResult> OnGetGradesAsync(int? courseId)
		{
			if (courseId == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var course = await _context.Courses.FirstOrDefaultAsync(c=> c.Id == courseId && c.InstructorId == instructorId);

			if (course == null)
				return NotFound();

			var coursesGrades = await _context.CourseGrades.FindByCriteriaAsync(cg=>cg.CourseId == courseId  );
			var courseGradeViewModels = _mapper.Map<IEnumerable<CourseGradeViewModel>>(coursesGrades);
			return new JsonResult(courseGradeViewModels);
		}

	}
}
