using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.SmartyDBContext;
using Smarty.Data.ViewModels.Courses;
using Smarty.Data.ViewModels.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Repositories.Implementations
{
	public class InstuctorsDashboardRepository : IInstuctorsDashboardRepository
	{
		private readonly SmartyDbContext _context;
		public InstuctorsDashboardRepository(SmartyDbContext context) 
		{
			_context = context;
		}

		public CourseAttendanceSummaryViewModel GetInstructorCoursesAttendanceSummary(int instructorId)
		{
			var summaryAsDynamicObject= (from sa in _context.StudentsAttendances
											join c in _context.Courses on sa.CourseId equals c.Id
											where c.InstructorId == instructorId
											group sa by c.Name into g
											select new 
											{
												CourseName = g.Key,
												PresentCount = g.Where(g => g.Status == AttendanceStatus.Present.ToString()).Count(),
												AbsentCount = g.Where(g => g.Status == AttendanceStatus.Absent.ToString()).Count(),
												ExcuseCount = g.Where(g => g.Status == AttendanceStatus.Excuse.ToString()).Count(),
											})
											.ToList();


			var summaryAsviewModel= new CourseAttendanceSummaryViewModel();
			foreach (var summaryItem in summaryAsDynamicObject)
			{
				summaryAsviewModel.CoursesNames.Add(summaryItem.CourseName);
				summaryAsviewModel.AbsentCount.Add(summaryItem.AbsentCount);
				summaryAsviewModel.ExcuseCount.Add(summaryItem.ExcuseCount);
				summaryAsviewModel.PresentCount.Add(summaryItem.PresentCount);
			}

			return summaryAsviewModel;
		}
		public CourseGradesAverageViewModel GetInstructorCoursesGradesAverage(int instructorId)
		{
			var gradesAverageAsDynamicObject = (from sg in _context.StudentsGrades
										join c in _context.Courses on sg.CourseId equals c.Id
										where c.InstructorId == instructorId
										group sg by new { c.Id, c.Name, sg.StudentId } into g
										select new
										{
											courseName = g.Key.Name,
											GradeTotal = g.Sum(g => g.Value)
										})
									   .GroupBy(c => c.courseName)
									   .Select(g => new {
										   CourseName= g.Key, 
										   CourseAverage= g.Sum(g => g.GradeTotal) / g.Count() 
									   })
									   .ToList();
			var gradesAverageAsViewModel = new CourseGradesAverageViewModel();

			foreach(var gradeAverageitem in gradesAverageAsDynamicObject)
			{
				gradesAverageAsViewModel.CoursesName.Add(gradeAverageitem.CourseName);
				gradesAverageAsViewModel.CoursesAverages.Add(gradeAverageitem.CourseAverage);
			}
			return gradesAverageAsViewModel;
		}

		

        public List<InstructorTimeTableViewModel> GetInstructorTimeTable(int instructorId)

        {
			var currentDay= DateTime.Now.DayOfWeek.ToString();
            var instructorTodayTimeTable = (from c in _context.Courses
                                            where c.InstructorId== instructorId && c.Day == currentDay
											orderby c.Time
                                            select new InstructorTimeTableViewModel
                                            { CourseName = c.Name, CourseTime = c.Time })
                                            .ToList();
            return instructorTodayTimeTable;

        }


		public int RegisterdStudentCountInInstructorCourses(int instructorId)
		{
			var count = (from c in _context.Courses
						 join sc in _context.StudentsCourses on c.Id equals sc.CourseId
						 where c.InstructorId == instructorId 
						 group sc by sc.StudentId into g
						 select g.Key).Count();	

			return count;

		}

		public int InstructorCoursesCount(int instructorId)
		{
			var count = (from c in _context.Courses
						 where c.InstructorId == instructorId
						 select c).Count();

			return count;
		}
	}
}
