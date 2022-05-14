using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.Instructors;

namespace Smarty.Pages.Dashboards
{
	[Authorize(Roles = nameof(Roles.Student))]

	public class StudentModel : PageModel
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _context;
		private readonly IToastNotification _toastr;
		private readonly UserManager<SmartyUser> _userManager;

		public StudentModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IToastNotification toastr)
		{
			_mapper = mapper;
			_context = context;
			_userManager = userManager;
			_toastr = toastr;
		}
		public IEnumerable<TimeTableViewModel> TimeTableViewModel { get; set; }
		public int StudentRegisterdCoursesCount { get; set; }
		public void OnGet()
		{
			var studentId = _userManager.GetUserAsync(User).Result.MemberId;

			TimeTableViewModel = _context.StudentsDashboard.GetStudentTimeTable(studentId);
			StudentRegisterdCoursesCount = _context.StudentsDashboard.GetStudentRegisterdCoursesCount(studentId);



		}
		public JsonResult OnGetDashboardCoursesAverage()
		{
			var studentId = _userManager.GetUserAsync(User).Result.MemberId;

			return new JsonResult(_context.StudentsDashboard.GetStudentCoursesGradesAverage(studentId));

		}
		public JsonResult OnGetDashboardCoursesAttendanceSummary()
		{
			var studentId = _userManager.GetUserAsync(User).Result.MemberId;

			return new JsonResult(_context.StudentsDashboard.GetStudentCoursesAttendanceSummary(studentId));

		}
	}
}
