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
	public class CreateModel : PageModel
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _context;
		private readonly IToastNotification _toastr;
		private readonly UserManager<SmartyUser> _userManager;
		public CreateModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IToastNotification toastr)
		{
			_mapper = mapper;
			_context = context;
			_userManager = userManager;
			_toastr = toastr;
		}
		[BindProperty]
		public CourseGradeFormViewModel ViewModel { get; set; }
		public SelectList SelectList { get; set; }
		public async Task<IActionResult> OnGet()
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var courses = await _context.Courses.FindByCriteriaAsync(c => c.InstructorId == instructorId);

			var selectListViewModel = _mapper.Map<IEnumerable<SelectCourseViewModel>>(courses);
			SelectList = new SelectList(selectListViewModel, "Id", "Name");
			return Page();
		}
		public async Task<IActionResult> OnPost()
		{

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;

			var courses = await _context.Courses.FindByCriteriaAsync(c => c.InstructorId == instructorId);
			var selectListViewModel = _mapper.Map<IEnumerable<SelectCourseViewModel>>(courses);
			SelectList = new SelectList(selectListViewModel, "Id", "Name");

			if (!ModelState.IsValid)
				return Page();

			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == ViewModel.CourseId && c.InstructorId == instructorId);
			if (existedCourse == null)
			{
				ModelState.AddModelError(String.Empty, "Course not exist");
				return Page();
			}

			var existedGrade = await _context.CourseGrades.FirstOrDefaultAsync(cg => cg.CourseId == ViewModel.CourseId && cg.Name == ViewModel.Name);
			if(existedGrade != null)
			{
				ModelState.AddModelError(String.Empty, "Grade already exist");
				return Page();
			}

			var courseGrade = _mapper.Map<CourseGrade>(ViewModel);
			_context.CourseGrades.Add(courseGrade);
			_context.SaveChanges();

			_toastr.AddSuccessToastMessage("Course Grade Added Successfully");
			return RedirectToPage("/CourseGrades/Index" , new {courseId = ViewModel.CourseId });

		}

	}
}
