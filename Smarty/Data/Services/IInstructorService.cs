using Microsoft.AspNetCore.Mvc.Rendering;

namespace Smarty.Data.Services
{
	public interface IInstructorService
	{
		Task<SelectList> GetCoursesSelectListAsync(int InstructorId , int? selectedCourseId = null);
	}
}
