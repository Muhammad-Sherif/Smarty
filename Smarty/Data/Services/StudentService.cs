using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.CourseGrades;
using Smarty.Data.ViewModels.Courses;

namespace Smarty.Data.Services
{
	public class StudentService : IStudentService
	{
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly UserManager<SmartyUser> _userManager;
		public StudentService(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<SelectList> GetCoursesSelectListAsync(int studentId, int? selectedCourseId = null)
		{
			var courses = _context.StudentsCourses.
				FindByCriteriaAsync(c => c.StudentId== studentId).Result
				.Select(sc => sc.Course);

			var selectListViewModel = _mapper.Map<IEnumerable<SelectCourseViewModel>>(courses);
			return new SelectList(selectListViewModel, "Id", "Description", selectedCourseId);

		}
	}
}
