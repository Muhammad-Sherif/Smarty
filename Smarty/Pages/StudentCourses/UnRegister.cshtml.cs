using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;

namespace Smarty.Pages.StudentCourses
{
	[Authorize(Roles = nameof(Roles.Student))]

	public class UnRegisterModel : PageModel
	{
		private readonly IUnitOfWork _context;
		private readonly UserManager<SmartyUser> _userManager;
		public UnRegisterModel(IUnitOfWork context, UserManager<SmartyUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		public async Task<IActionResult> OnPostAsync(int? courseId)
		{
			if (courseId == null)
				return BadRequest();

			var studentId = _userManager.GetUserAsync(User).Result.MemberId;
			var studentCourse = await _context.StudentsCourses.FirstOrDefaultAsync(c => c.CourseId == courseId && c.StudentId == studentId);

			if (studentCourse == null)
				return NotFound();

			_context.StudentsCourses.Delete(studentCourse);
			_context.SaveChanges();
			return new OkResult();
		}
	}
}
