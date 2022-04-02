using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.ViewModels.CourseAttendances;
using Smarty.Data.ViewModels.CourseGrades;

namespace Smarty.Pages.CourseAttendances
{

	[Authorize(Roles = nameof(Roles.Instructor))]

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

		public SelectList SelectList;

		public async Task OnGetAsync(int? selectedCourseId)
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instructorService.GetCoursesSelectListAsync(instructorId, selectedCourseId);

		}
		public async Task<IActionResult> OnGetAttendancesAsync(int? courseId)
		{
			if (courseId == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);

			if (course == null)
				return NotFound();

			var coursesAttendances = await _context.CourseAttendances.FindByCriteriaAsync(cg => cg.CourseId == courseId);
			var courseAttendanceViewModels = _mapper.Map<IEnumerable<CourseAttendanceViewModel>>(coursesAttendances);
			return new JsonResult(courseAttendanceViewModels);
		}
	}
}
