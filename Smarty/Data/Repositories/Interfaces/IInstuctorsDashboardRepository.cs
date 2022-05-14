using Smarty.Data.Models;
using Smarty.Data.ViewModels.Courses;
using Smarty.Data.ViewModels.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Repositories.Interfaces
{
	public interface IInstuctorsDashboardRepository 
	{
		CourseAttendanceSummaryViewModel GetInstructorCoursesAttendanceSummary(int instructorId);
		CourseGradesAverageViewModel GetInstructorCoursesGradesAverage(int instructorId);
		List<InstructorTimeTableViewModel> GetInstructorTimeTable(int instructorId);
		int InstructorCoursesCount(int instructorId);
		int RegisterdStudentCountInInstructorCourses(int instructorId);




	}
}
