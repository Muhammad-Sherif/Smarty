using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.Courses;

namespace Smarty.Pages.StudentCourses
{
    public class RegisterModel : PageModel
    {
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _context;
		private readonly IToastNotification _toastr;
		private readonly UserManager<SmartyUser> _userManager;
		public RegisterModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IToastNotification toastr)
		{
			_mapper = mapper;
			_context = context;
			_userManager = userManager;
			_toastr = toastr;
		}
		[BindProperty]
		public RegisterCourseFromViewModel ViewModel { get; set; }
		public void OnGetAsync()
		{

		}
		public async Task<IActionResult> OnPostAsync()
        {
			if (!ModelState.IsValid)
				return Page();

			var studentId = _userManager.GetUserAsync(User).Result.MemberId;
			var course = await _context.Courses.FirstOrDefaultAsync(c => c.RegisterCode == ViewModel.RegisterCode);

			if (course == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid register code");
				return Page();
			}
			var existedStudentCourse = await _context.StudentsCourses.FirstOrDefaultAsync(c=>c.CourseId == course.Id && c.StudentId==studentId ) ;
			if(existedStudentCourse != null)
			{
				ModelState.AddModelError(string.Empty, "You already registered in this course");
				return Page();
			}
			var studentCourse = new StudentsCourses() { CourseId = course.Id, StudentId = studentId };
			_context.StudentsCourses.Add(studentCourse);
			_context.SaveChanges();
			_toastr.AddSuccessToastMessage("Register done Successfully");
			return RedirectToPage("Index");

		}
    }
}
