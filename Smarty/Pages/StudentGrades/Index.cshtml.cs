using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.CourseGrades;
using Smarty.Data.ViewModels.StudentGrades;

namespace Smarty.Pages.StudentGrades
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

        public SelectList SelectList;

		public async Task OnGet(int? courseId)
        {
            var studentId = _userManager.GetUserAsync(User).Result.MemberId;
            var studentCourses = await _context.StudentsCourses.FindByCriteriaAsync(sc => sc.StudentId == studentId , sc => sc.Course);
            var courses = studentCourses.Select(sc => sc.Course);
            var selectListViewModel = _mapper.Map<IEnumerable<SelectCourseViewModel>>(courses);
            SelectList = new SelectList(selectListViewModel, "Id", "Description" , courseId);
        }
        public async Task<IActionResult> OnGetGrades(int? courseId)
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
