using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.StudentAttendances;

namespace Smarty.Pages.StudentAttendances
{
    public class EditModel : PageModel
    {
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly IToastNotification _toastr;
		private readonly UserManager<SmartyUser> _userManager;

		public EditModel(IUnitOfWork context,
			IMapper mapper,
			UserManager<SmartyUser> userManager,
			IToastNotification toastr)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
			_toastr = toastr;
		}
		[BindProperty]
		public StudentAttendanceFormViewModel ViewModel { get; set; }
		public async Task<IActionResult> OnGetAsync(int? studentId, int? courseId, DateTime? dateTime)
		{
			if (studentId == null || courseId == null || dateTime == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;

			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);
			if (existedCourse == null)
				return NotFound();

			var studentAttendance = await _context.StudentsAttendances.FirstOrDefaultAsync(sg =>
			   sg.StudentId == studentId &&
			   sg.CourseId == courseId && sg.DateTime == dateTime);

			if (studentAttendance == null)
				return NotFound();

			ViewModel = _mapper.Map<StudentAttendanceFormViewModel>(studentAttendance);
			return Page();

		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
				return Page();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == ViewModel.CourseId && c.InstructorId == instructorId);
			if (existedCourse == null)
				return NotFound();

			var studentAttendance = await _context.StudentsAttendances.FirstOrDefaultAsync(sg =>
			   sg.StudentId == ViewModel.StudentId &&
			   sg.CourseId == ViewModel.CourseId && sg.DateTime== ViewModel.DateTime,
			   sg => sg.CourseAttendance);

			if (studentAttendance == null)
				return NotFound();


			studentAttendance = _mapper.Map(ViewModel, studentAttendance);
			_context.SaveChanges();
			_toastr.AddSuccessToastMessage("Student Attendance Edited Successfully");
			return RedirectToPage("/StudentAttendances/Instructor");


		}


	}
}
