using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.ViewModels.CourseAttendances;
using Smarty.Data.ViewModels.CourseGrades;

namespace Smarty.Pages.CourseAttendances
{
	public class CreateModel : PageModel
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _context;
		private readonly IToastNotification _toastr;
		private readonly IInstructorService _instuctorService;
		private readonly UserManager<SmartyUser> _userManager;
		public CreateModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IToastNotification toastr, IInstructorService instuctorService)
		{
			_mapper = mapper;
			_context = context;
			_userManager = userManager;
			_toastr = toastr;
			_instuctorService = instuctorService;

		}
		[BindProperty]
		public CourseAttendanceFormViewModel ViewModel { get; set; }
		public string LocationAccessErrorUrl { get; set; }
		public SelectList SelectList { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instuctorService.GetCoursesSelectListAsync(instructorId);
			LocationAccessErrorUrl = Url.Page(
			pageName: "/CourseAttendances/LocationAccessError",
			pageHandler: null,
			values: null,
			protocol: Request.Scheme);
			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{

			if(ViewModel.Latitude == 0 || ViewModel.Longitude== 0)
			{
				return RedirectToPage("/CourseAttendances/LocationAccessError",new { codeError = 1 });
			}

			if (!ModelState.IsValid)
				return Page();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instuctorService.GetCoursesSelectListAsync(instructorId);

			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == ViewModel.CourseId && c.InstructorId == instructorId);
			if (existedCourse == null)
			{
				ModelState.AddModelError(String.Empty, "Course not exist");
				return Page();
			}

			var existedCourseAttendance =
				await _context.CourseAttendances.FindByKeyAsync(ViewModel.DateTime, ViewModel.CourseId);

			if (existedCourseAttendance != null)
			{
				ModelState.AddModelError(String.Empty, "Course Attendance is exist");
				return Page();
			}

			var courseAttendance = _mapper.Map<CourseAttendance>(ViewModel);

			_context.CourseAttendances.Add(courseAttendance);
			_context.SaveChanges();
			_toastr.AddSuccessToastMessage("Course Attendance Added Successfully");
			return RedirectToPage("/CourseAttendances/Index", new { selectedCourseId = ViewModel.CourseId });

		}
	}
}
