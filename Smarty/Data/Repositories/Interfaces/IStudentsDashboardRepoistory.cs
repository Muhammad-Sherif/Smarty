using Smarty.Data.ViewModels.Courses;
using Smarty.Data.ViewModels.Instructors;

namespace Smarty.Data.Repositories.Interfaces
{
	public interface IStudentsDashboardRepoistory
	{
		CourseAttendanceSummaryViewModel GetStudentCoursesAttendanceSummary(int studentId);
		CourseGradesTotalViewModel GetStudentCoursesGradesAverage(int studentId);
		List<TimeTableViewModel> GetStudentTimeTable(int studentId);
		int GetStudentRegisterdCoursesCount(int studentId);
	}
}
