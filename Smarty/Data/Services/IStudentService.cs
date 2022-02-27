using Microsoft.AspNetCore.Mvc.Rendering;

namespace Smarty.Data.Services
{
	public interface IStudentService
	{
		Task<SelectList> GetCoursesSelectListAsync(int studentId, int? selectedCourseId = null);

	}
}
