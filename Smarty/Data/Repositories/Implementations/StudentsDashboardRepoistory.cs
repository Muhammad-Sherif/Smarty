using Smarty.Data.Enums;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.SmartyDBContext;
using Smarty.Data.ViewModels.Courses;
using Smarty.Data.ViewModels.Instructors;

namespace Smarty.Data.Repositories.Implementations
{
	public class StudentsDashboardRepoistory : IStudentsDashboardRepoistory
	{
		private readonly SmartyDbContext _context;
		public StudentsDashboardRepoistory(SmartyDbContext context)
		{
			_context = context;
		}

		public List<TimeTableViewModel> GetStudentTimeTable(int studentId)
		{

			var currentDay = DateTime.Now.DayOfWeek.ToString();

			var studentTodayTimeTable = (from sc in _context.StudentsCourses
										  join c in _context.Courses on sc.CourseId equals c.Id
										  where sc.StudentId == studentId && c.Day  == currentDay
										  select new TimeTableViewModel 
										  {	 
											CourseName = c.Name , CourseTime = c.Time
										  })
										  .ToList();

			return studentTodayTimeTable;

		}

		public CourseAttendanceSummaryViewModel GetStudentCoursesAttendanceSummary(int studentId)
		{
			var summaryAsDynamicObject = (from sa in _context.StudentsAttendances
										  join c in _context.Courses on sa.CourseId equals c.Id
										  where sa.StudentId == studentId
										  group sa by new { c.Name  , c.Id} into g
										  select new
										  {
											  CourseName = g.Key.Name,
											  PresentCount = g.Where(g => g.Status == AttendanceStatus.Present.ToString()).Count(),
											  AbsentCount = g.Where(g => g.Status == AttendanceStatus.Absent.ToString()).Count(),
											  ExcuseCount = g.Where(g => g.Status == AttendanceStatus.Excuse.ToString()).Count(),
										  })
											.ToList();


			var summaryAsviewModel = new CourseAttendanceSummaryViewModel();
			foreach (var summaryItem in summaryAsDynamicObject)
			{
				summaryAsviewModel.CoursesNames.Add(summaryItem.CourseName);
				summaryAsviewModel.AbsentCount.Add(summaryItem.AbsentCount);
				summaryAsviewModel.ExcuseCount.Add(summaryItem.ExcuseCount);
				summaryAsviewModel.PresentCount.Add(summaryItem.PresentCount);
			}
			return summaryAsviewModel;
		}

		public CourseGradesTotalViewModel GetStudentCoursesGradesAverage(int studentId)
		{
			var gradesAverageAsDynamicObject = (from sg in _context.StudentsGrades
												join c in _context.Courses on sg.CourseId equals c.Id
												where sg.StudentId == studentId
												group sg by new { c.Id, c.Name  } into g
												select new
												{
													courseName = g.Key.Name,
													GradeTotal = g.Sum(g => g.Value)
												}).ToList();

			var gradesTotalAsViewModel = new CourseGradesTotalViewModel();

			foreach (var gradeTotalitem in gradesAverageAsDynamicObject)
			{
				gradesTotalAsViewModel.CoursesName.Add(gradeTotalitem.courseName);
				gradesTotalAsViewModel.CoursesTotals.Add(gradeTotalitem.GradeTotal);
			}
			return gradesTotalAsViewModel;
		}

		public int GetStudentRegisterdCoursesCount(int studentId)
		{
			var count = (from sc in _context.StudentsCourses
						 where sc.StudentId == studentId
						 select sc).Count();

			return count;
		}
	}
}
