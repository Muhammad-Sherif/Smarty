using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.Courses;

namespace Smarty.Pages.Courses
{
    [Authorize(Roles = nameof(Roles.Instructor))]

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
        public int? CourseId { get; set; }

        [BindProperty]
        public CourseFormViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
                return BadRequest();
            var instructorId = _userManager.GetUserAsync(User).Result.MemberId;

            var course = await _context.Courses.FirstOrDefaultAsync(c=>c.Id == id && c.InstructorId == instructorId);
            if (course == null)
                return NotFound();

            ViewModel = _mapper.Map<CourseFormViewModel>(course);
            CourseId = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == CourseId && c.InstructorId == instructorId);
            if (course == null)
                return NotFound();

            var existedCodeCourse = _context.Courses.FirstOrDefault(c => c.Code == course.Code && c.Id != CourseId);
            if (existedCodeCourse != null)
            {
                ModelState.AddModelError(string.Empty, "This course code is used for another courses");
                return Page();
            }
            course = _mapper.Map(ViewModel,course);
            _context.SaveChanges();
            _toastr.AddSuccessToastMessage("Course Updated Successfully");
            return RedirectToPage("Index");
        }


    }
}
