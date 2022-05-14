using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.ViewModels.Courses
{
	public class CourseAttendanceSummaryViewModel
	{
		public List<string> CoursesNames { get; set; } = new List<string>();
		public List<int> PresentCount { get; set; } = new List<int>();
		public List<int> AbsentCount { get; set; } = new List<int>();
		public List<int> ExcuseCount { get; set; } = new List<int>();

	}
}
