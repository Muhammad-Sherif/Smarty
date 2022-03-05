using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using System.Drawing;

namespace Smarty.Pages.CourseAttendances
{
    public class QrCodeModel : PageModel
    {
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly UserManager<SmartyUser> _userManager;

		public QrCodeModel(IUnitOfWork context,
			IMapper mapper,
			UserManager<SmartyUser> userManager)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
		}
		public string QrCodeUrl { get; set; }

		public async Task<IActionResult> OnGet(int? courseId , DateTime? dateTime)
        {
            if (courseId == null || dateTime == null)
                BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);
			if (existedCourse == null)
				return NotFound();

			var courseAttendance = await _context.CourseAttendances.FindByKeyAsync(dateTime, courseId);
			if (courseAttendance == null)
				return NotFound();

			QrCodeUrl = Url.Page(
				pageName:"/CourseAttendances/Index",
				pageHandler:null,
				values: new { courseId = courseAttendance.CourseId, dateTime = courseAttendance.DateTime.ToString("M-d-yyyy") },
				protocol: Request.Scheme);

			return Page();

        }
    }
}
