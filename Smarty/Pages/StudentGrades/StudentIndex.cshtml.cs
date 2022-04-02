using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.ViewModels.CourseGrades;
using Smarty.Data.ViewModels.StudentGrades;

namespace Smarty.Pages.StudentGrades
{
    [Authorize(Roles = nameof(Roles.Student))]

    public class StudentIndexModel : PageModel
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly UserManager<SmartyUser> _userManager;
        private readonly IStudentService _studentService;



		public StudentIndexModel(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager, IStudentService studentService)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
			_studentService = studentService;
		}

		public SelectList SelectList;

		public async Task OnGetAsync(int? selectedCourseId)
        {
            var studentId = _userManager.GetUserAsync(User).Result.MemberId;
            SelectList = await _studentService.GetCoursesSelectListAsync(studentId, selectedCourseId);

        }
        public async Task<IActionResult> OnGetGradesAsync(int? courseId)
        {
            if (courseId == null)
                BadRequest();

            var studentId = _userManager.GetUserAsync(User).Result.MemberId;
            var existedStudentCourse = await _context.StudentsCourses.FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if (existedStudentCourse == null)
                return NotFound();

            var studentGrades = await _context.StudentsGrades.FindByCriteriaAsync(sg => sg.StudentId == studentId && sg.CourseId == courseId , sg=>sg.CourseGrade);
            var studentGradeViewModels = _mapper.Map<IEnumerable<StudentGradeViewModel>>(studentGrades);
            return new JsonResult(studentGradeViewModels);

        }
    }
}
