using AutoMapper;
using Geolocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.CourseAttendances;
using Smarty.Data.ViewModels.StudentAttendances;

namespace Smarty.Pages.StudentAttendances
{
	[Authorize(Roles = nameof(Roles.Student))]

	public class ScanQrCodeModel : PageModel
	{
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly IToastNotification _toastr;
		private readonly UserManager<SmartyUser> _userManager;

		public ScanQrCodeModel(IUnitOfWork context,
			IMapper mapper,
			UserManager<SmartyUser> userManager, IToastNotification toastr)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
			_toastr = toastr;
		}
		[BindProperty]
		public CourseAttendanceFormViewModel ViewModel { get; set; }
		public string LocationAccessErrorUrl { get;  set; }
		public async Task<IActionResult> OnGetAsync(int? courseId, DateTime? dateTime)
		{
			if (courseId == null || dateTime == null)
				BadRequest();

			var existedCourseAttendance = await _context.CourseAttendances.FirstOrDefaultAsync(c => c.CourseId == courseId && c.DateTime== dateTime);
			if (existedCourseAttendance == null)
				return NotFound();

			var studentId = _userManager.GetUserAsync(User).Result.MemberId;
			var existedStudentCourse = await _context.StudentsCourses.FirstOrDefaultAsync(c => c.CourseId == courseId && c.StudentId == studentId);
			if (existedStudentCourse == null)
				return NotFound();

			ViewModel = new CourseAttendanceFormViewModel();
			ViewModel.CourseId = courseId.Value;
			ViewModel.DateTime = dateTime.Value;
			LocationAccessErrorUrl = Url.Page(
			pageName: "/CourseAttendances/LocationAccessError",
			pageHandler: null,
			values: null,
			protocol: Request.Scheme);
			return Page();

		}
		public async Task<IActionResult> OnPostAsync()
		{

			if (ViewModel.Latitude == 0 || ViewModel.Longitude == 0)
			{
				return RedirectToPage("/CourseAttendances/LocationAccessError", new { codeError = 1 });
			}

			if (!ModelState.IsValid)
				return Page();
			var studentId= _userManager.GetUserAsync(User).Result.MemberId;

			var studentAttendance = await _context.StudentsAttendances
				.FirstOrDefaultAsync(c => 
				c.CourseId == ViewModel.CourseId && 
				c.DateTime == ViewModel.DateTime && 
				c.StudentId == studentId);

			if (studentAttendance == null)
				return NotFound();

			var courseAttendance = await _context.CourseAttendances.FindByKeyAsync(ViewModel.DateTime, ViewModel.CourseId);

			if(!courseAttendance.QRCodeEnabled)
			{
				_toastr.AddErrorToastMessage("Qr code scan disabled");
				return RedirectToPage("/StudentAttendances/Student", new { selectedCourseId = ViewModel.CourseId });
			}

			var distanceBetweenInstructorAndStudent = GeoCalculator.GetDistance(
				ViewModel.Latitude,ViewModel.Longitude , 
				courseAttendance.Latitude , courseAttendance.Longitude
				,2
				,DistanceUnit.Meters
				);

			if(distanceBetweenInstructorAndStudent > courseAttendance.AcceptedScanDistance)
			{
				_toastr.AddErrorToastMessage("you should be in lecture to take attendance");
				return RedirectToPage("/StudentAttendances/Student", new { selectedCourseId = ViewModel.CourseId });
			}
			studentAttendance.Status = AttendanceStatus.Present.ToString();
			_context.SaveChanges();
			_toastr.AddSuccessToastMessage("Attendance taked successfully");
			return RedirectToPage("/StudentAttendances/Student", new { selectedCourseId = ViewModel.CourseId });

		}


	}
}
