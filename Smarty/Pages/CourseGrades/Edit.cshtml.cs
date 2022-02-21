using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.CourseGrades;

namespace Smarty.Pages.CourseGrades
{
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
		public CourseGradeFormViewModel ViewModel { get; set; }
		public async Task<IActionResult> OnGet(int? courseId , string name)
		{

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;

			if (courseId == null || name == null)
				return BadRequest();


			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);
			if (existedCourse == null)
				return NotFound();

			var existedGrade = await _context.CourseGrades.FirstOrDefaultAsync(cg => cg.CourseId == courseId && cg.Name == name);
			if (existedGrade == null)
				return NotFound();

			ViewModel = _mapper.Map<CourseGradeFormViewModel>(existedGrade);
			return Page();
		}
		public async Task<IActionResult> OnPost()
		{


			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;

			if (!ModelState.IsValid)
				return Page();

			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == ViewModel.CourseId && c.InstructorId == instructorId);
			if (existedCourse == null)
			{
				ModelState.AddModelError(String.Empty, "Course not exist");
				return Page();
			}

			var existedGrade = await _context.CourseGrades.FirstOrDefaultAsync(cg => cg.CourseId == ViewModel.CourseId && cg.Name == ViewModel.Name);
			if (existedGrade == null)
			{
				ModelState.AddModelError(String.Empty, "Grade not exist");
				return Page();
			}

			existedGrade = _mapper.Map(ViewModel, existedGrade);
			_context.SaveChanges();

			_toastr.AddSuccessToastMessage("Course Grade Edited Successfully");
			return RedirectToPage("/CourseGrades/Index", new { courseId = ViewModel.CourseId });

		}

	}
}
