using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.CourseGrades;
using Smarty.Data.ViewModels.StudentGrades;
using Smarty.Data.ViewModels.Students;

namespace Smarty.Pages.StudentGrades
{
    public class EditModel : PageModel
    {
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly UserManager<SmartyUser> _userManager;
		public EditModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
		}
		public SelectList SelectList;
		public async Task<IActionResult> OnGet(int? courseId)
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var courses = await _context.Courses.FindByCriteriaAsync(c => c.InstructorId == instructorId);

			var selectListViewModel = _mapper.Map<IEnumerable<SelectCourseViewModel>>(courses);
			SelectList = new SelectList(selectListViewModel, "Id", "Description", courseId);
			return Page();

		}

		public async Task<IActionResult> OnGetStudents(int? courseId)
		{
			if (courseId == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);

			if (existedCourse == null)
				return NotFound();

			var studentsCourses = await _context.StudentsCourses.FindByCriteriaAsync(sg => sg.CourseId == courseId , sg=>sg.Student);
			var students = studentsCourses.Select(sc => sc.Student);
			var SelectListViewModel = _mapper.Map<IEnumerable<SelectStudentViewModel>>(students);
			return new JsonResult(SelectListViewModel);
		}
		public async Task<IActionResult> OnGetGrades(int? courseId , int? studentId)
		{
			if (courseId == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);

			if (existedCourse == null)
				return NotFound();

			var existedStudentCourse = await _context.StudentsCourses.FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
			if (existedStudentCourse == null)
				return NotFound();

			var studentGrades = await _context.StudentsGrades.FindByCriteriaAsync(sg => sg.StudentId == studentId && sg.CourseId == courseId , sg=>sg.CourseGrade);
			var studentGradeViewModels = _mapper.Map<IEnumerable<StudentGradeViewModel>>(studentGrades);
			return new JsonResult(studentGradeViewModels);

		}
	}
}
