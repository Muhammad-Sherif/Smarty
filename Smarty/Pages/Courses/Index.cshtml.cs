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

    public class IndexModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;
        private readonly IToastNotification _toastr;
        private readonly UserManager<SmartyUser> _userManager;
        public IndexModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IToastNotification toastr)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _toastr = toastr;
        }
		public IEnumerable<InstructorCourseViewModel> ViewModel { get; set; }

		public async Task OnGetAsync()
        {
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var courses = await _context.Courses.FindByCriteriaAsync(c => c.InstructorId == instructorId);
            ViewModel = _mapper.Map<IEnumerable<InstructorCourseViewModel>>(courses);

		}

	}
}
