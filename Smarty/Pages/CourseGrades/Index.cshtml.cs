using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.SmartyDBContext;
using Smarty.Data.ViewModels.CourseGrades;

namespace Smarty.Pages.CourseGrades
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly UserManager<SmartyUser> _userManager;
		public IndexModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
		}
		public SelectList  SelectList;
		public async Task<IActionResult> OnGet(int? courseId)
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var courses = await _context.Courses.FindByCriteriaAsync(c => c.InstructorId == instructorId);

			var selectListViewModel = _mapper.Map<IEnumerable<SelectCourseViewModel>>(courses);
			SelectList = new SelectList(selectListViewModel , "Id" , "Description" , courseId);
			return Page();
		}
		public async Task<IActionResult> OnGetGrades(int? courseId)
		{
			if (courseId == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var course = await _context.Courses.FirstOrDefaultAsync(c=> c.Id == courseId && c.InstructorId == instructorId);

			if (course == null)
				return NotFound();

			var coursesGrades = await _context.CourseGrades.FindByCriteriaAsync(cg=>cg.CourseId == courseId  );
			var courseGradeViewModels = _mapper.Map<IEnumerable<CourseGradeViewModel>>(coursesGrades);
			return new JsonResult(courseGradeViewModels);
		}

	}
}
