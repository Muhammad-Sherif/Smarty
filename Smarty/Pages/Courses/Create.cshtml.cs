using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.SmartyDBContext;
using Smarty.Data.ViewModels.Courses;

namespace Smarty.Pages.Courses
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
        public CourseViewModel ViewModel { get; set; }


        public void OnGet()
        {

        }

        public IActionResult OnPostAsync()
        {
            var course = _mapper.Map<Course>(ViewModel);
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var existedCourse = _context.Courses.FirstOrDefault(c => c.Code == course.Code);
            if(existedCourse != null)
            {
                ModelState.AddModelError(string.Empty, "Use an unique code");
                return Page();
            }

            var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
            course.InstructorId = instructorId;
            _context.Courses.Add(course);
            _context.SaveChanges();

            _toastr.AddSuccessToastMessage("Course Added Successfully");

            return RedirectToPage("/Index");
        }

    }
}
