using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.ViewModels.CourseGrades;
using Smarty.Data.ViewModels.StudentGrades;
using Smarty.Data.ViewModels.Students;

namespace Smarty.Pages.StudentGrades
{
    public class InstructorIndexModel : PageModel
    {
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly UserManager<SmartyUser> _userManager;
		private readonly IInstructorService _instructorService;

		public InstructorIndexModel(IUnitOfWork context, 
			IMapper mapper, 
			UserManager<SmartyUser> userManager, 
			IInstructorService instructorService)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
			_instructorService = instructorService;
		}

		public SelectList SelectList;

		public async Task OnGetAsync(int? selectedCourseId)
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instructorService.GetCoursesSelectListAsync(instructorId, selectedCourseId);

		}

		public async Task<IActionResult> OnGetStudentsAsync(int? courseId)
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
		public async Task<IActionResult> OnGetGradesAsync(int? courseId , int? studentId)
		{
			if (courseId == null || studentId == null)
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
