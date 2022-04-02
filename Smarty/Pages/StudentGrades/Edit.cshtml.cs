using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.ViewModels.StudentGrades;

namespace Smarty.Pages.StudentGrades
{
	[Authorize(Roles = nameof(Roles.Instructor))]

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
		public StudentGradeFormViewModel ViewModel { get; set; }
		public double MaxGradeValue { get; set; }
		public async Task<IActionResult> OnGetAsync(int? studentId, int? courseId, string name)
		{
			if (studentId == null || courseId == null || name == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;

			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);
			if (existedCourse == null)
				return NotFound();

			var studentGrade = await _context.StudentsGrades.FirstOrDefaultAsync(sg =>
			   sg.StudentId == studentId &&
			   sg.CourseId == courseId && sg.Name == name,
			   sg => sg.CourseGrade);

			if (studentGrade == null)
				return NotFound();

			MaxGradeValue = studentGrade.CourseGrade.MaxValue;
			ViewModel = _mapper.Map<StudentGradeFormViewModel>(studentGrade);
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

			var studentGrade = await _context.StudentsGrades.FirstOrDefaultAsync(sg =>
			   sg.StudentId == ViewModel.StudentId &&
			   sg.CourseId == ViewModel.CourseId && sg.Name == ViewModel.Name,
			   sg => sg.CourseGrade);

			if (studentGrade == null)
				return NotFound();

			MaxGradeValue = studentGrade.CourseGrade.MaxValue;
			if (ViewModel.Value > MaxGradeValue)
			{
				ModelState.AddModelError("", $"Grade must be not greater than {MaxGradeValue}");
				return Page();
			}
			studentGrade = _mapper.Map(ViewModel, studentGrade);
			_context.SaveChanges();
			_toastr.AddSuccessToastMessage("Student Grade Edited Successfully");
			return RedirectToPage("/StudentGrades/InstructorIndex");


		}

	}
}
