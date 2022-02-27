using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.ViewModels.CourseGrades;

namespace Smarty.Data.Services
{
	public class InstructorService : IInstructorService
	{
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly UserManager<SmartyUser> _userManager;
		public InstructorService(IUnitOfWork context, IMapper mapper, UserManager<SmartyUser> userManager)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<SelectList> GetCoursesSelectListAsync(int instructorId,int? selectedCourseId =null )
		{
			var courses = _context
				.StudentsCourses
				.FindByCriteriaAsync(c => c.StudentId == instructorId).Result
				.Select(sc => sc.Course);

			var selectListViewModel = _mapper.Map<IEnumerable<SelectCourseViewModel>>(courses);
			return new SelectList(selectListViewModel, "Id", "Description", selectedCourseId);

		}
	}
}
