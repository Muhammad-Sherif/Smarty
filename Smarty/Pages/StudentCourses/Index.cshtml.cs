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

namespace Smarty.Pages.StudentCourses
{
    [Authorize(Roles = nameof(Roles.Student))]

    public class IndexModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;
        private readonly IToastNotification _toastr;
        private readonly UserManager<SmartyUser> _userManager;
        public IndexModel (IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IToastNotification toastr)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _toastr = toastr;
        }
        public IEnumerable<StudentCourseViewModel> ViewModel { get; set; }

        public async Task OnGetAsync()
        {
            var studentId = _userManager.GetUserAsync(User).Result.MemberId;
            var studentCourses= await _context.StudentsCourses.FindByCriteriaAsync(c => c.StudentId == studentId , c=>c.Course);
            var courses = studentCourses.Select(c => c.Course);
            ViewModel = _mapper.Map<IEnumerable<StudentCourseViewModel>>(courses);

        }
        

    }
}
