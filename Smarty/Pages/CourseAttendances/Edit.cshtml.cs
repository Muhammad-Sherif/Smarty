using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.CourseAttendances;

namespace Smarty.Pages.CourseAttendances
{
	[Authorize(Roles = nameof(Roles.Instructor))]
	public class EditModel : PageModel
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _context;
		private readonly IToastNotification _toastr;
		private readonly UserManager<SmartyUser> _userManager;
		public EditModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IToastNotification toastr)
		{
			_mapper = mapper;
			_context = context;
			_userManager = userManager;
			_toastr = toastr;
		}
		[BindProperty]
		public CourseAttendanceEditFormViewModel ViewModel { get; set; }
		public async Task<IActionResult> OnGetAsync(int? courseId, DateTime? dateTime)
		{

			if (courseId == null || dateTime == null)
				return BadRequest();


			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);
			if (existedCourse == null)
				return NotFound();

			var existedCourseAttendance = await _context.CourseAttendances.FirstOrDefaultAsync(cg => cg.CourseId == courseId && cg.DateTime== dateTime);
			if (existedCourseAttendance == null)
				return NotFound();

			ViewModel = _mapper.Map<CourseAttendanceEditFormViewModel>(existedCourseAttendance);
			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{



			if (!ModelState.IsValid)
				return Page();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == ViewModel.CourseId && c.InstructorId == instructorId);
			if (existedCourse == null)
			{
				ModelState.AddModelError(String.Empty, "Course not exist");
				return Page();
			}

			var existedCourseAttendances = await _context.CourseAttendances.FindByKeyAsync( ViewModel.DateTime , ViewModel.CourseId);
			if (existedCourseAttendances == null)
			{
				ModelState.AddModelError(String.Empty, "CourseAttendances  not exist");
				return Page();
			}

			existedCourseAttendances = _mapper.Map(ViewModel, existedCourseAttendances);
			_context.SaveChanges();

			_toastr.AddSuccessToastMessage("Course Attendance Edited Successfully");
			return RedirectToPage("/CourseAttendances/Index", new { selectedCourseId = ViewModel.CourseId });

		}

	}
}
