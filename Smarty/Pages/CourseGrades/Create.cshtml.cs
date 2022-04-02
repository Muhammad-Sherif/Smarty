using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.ViewModels.CourseGrades;
using Smarty.Data.ViewModels.Courses;

namespace Smarty.Pages.CourseGrades
{
	[Authorize(Roles = nameof(Roles.Instructor))]

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
		public CourseGradeFormViewModel ViewModel { get; set; }
		public SelectList SelectList { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instuctorService.GetCoursesSelectListAsync(instructorId);
			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instuctorService.GetCoursesSelectListAsync(instructorId);

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
			return RedirectToPage("/CourseGrades/Index" , new {selectedCourseId = ViewModel.CourseId });

		}

	}
}
