using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;

namespace Smarty.Pages.Courses
{
	[Authorize(Roles = nameof(Roles.Instructor))]

	public class DeleteModel : PageModel
    {
		private readonly IUnitOfWork _context;
		private readonly UserManager<SmartyUser> _userManager;
		public DeleteModel(IUnitOfWork context, UserManager<SmartyUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}


		public async Task<IActionResult> OnPostAsync(int? courseId)
		{
			if (courseId == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var course = await _context.Courses.FirstOrDefaultAsync(c=>c.Id==courseId &&  c.InstructorId == instructorId);

			if (course == null)
				return NotFound();

			_context.Courses.Delete(course);
			_context.SaveChanges();
			return new OkResult();
		}


    }
}
