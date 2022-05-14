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
using System.Text.Json;

namespace Smarty.Pages.DashBoards
{
	[Authorize(Roles = nameof(Roles.Instructor))]
	public class InstructorModel : PageModel
	{

		private readonly IMapper _mapper;
		private readonly IUnitOfWork _context;
		private readonly IToastNotification _toastr;
		private readonly UserManager<SmartyUser> _userManager;

		public InstructorModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IToastNotification toastr)
		{
			_mapper = mapper;
			_context = context;
			_userManager = userManager;
			_toastr = toastr;
		}
		public IEnumerable<TimeTableViewModel> TimeTableViewModel { get; set; }
		public int RegisterdStudentCount { get; set; }
		public int InsturctorCoursesCount { get; set; }

		public void OnGet()
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;

			TimeTableViewModel = _context.InstructorsDashboard.GetInstructorTimeTable(instructorId);

			RegisterdStudentCount = _context.InstructorsDashboard.RegisterdStudentCountInInstructorCourses(instructorId);
			InsturctorCoursesCount = _context.InstructorsDashboard.InstructorCoursesCount(instructorId);

		}
		public JsonResult OnGetDashboardCoursesAverage()
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;

			return new JsonResult(_context.InstructorsDashboard.GetInstructorCoursesGradesAverage(instructorId));

		}
		public JsonResult OnGetDashboardCoursesAttendanceSummary()
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			return new JsonResult(_context.InstructorsDashboard.GetInstructorCoursesAttendanceSummary(instructorId));

		}

	}
}
