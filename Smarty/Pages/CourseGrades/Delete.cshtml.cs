using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;

namespace Smarty.Pages.CourseGrades
{
    public class DeleteModel : PageModel
    {
		private readonly IUnitOfWork _context;
		private readonly UserManager<SmartyUser> _userManager;
		public DeleteModel(IUnitOfWork context,  UserManager<SmartyUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		
        

		public async Task<IActionResult> OnPost(int? courseId , string name)
		{
			if (courseId == null || name == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);
			if (course == null)
				return NotFound();


			var courseGrade = await _context.CourseGrades.FindByKeyAsync(name ,courseId);
			if (courseGrade == null)
				return NotFound();


			_context.CourseGrades.Delete(courseGrade);
			_context.SaveChanges();
			return new OkResult();
		}
	}
}
