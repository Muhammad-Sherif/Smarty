using EntityFrameworkCore.Triggered;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;

namespace Smarty.Data.Triggers
{
	public class AddRelatedCourseAttendanceDataTrigger : IBeforeSaveTrigger<CourseAttendance>
	{
		public IUnitOfWork _context { get; set; }
		public AddRelatedCourseAttendanceDataTrigger(IUnitOfWork context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<CourseAttendance> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added)
			{
				var entity = context.Entity;
				var studentsCourses = _context.StudentsCourses.FindByCriteriaAsync(sc => sc.CourseId == entity.CourseId).Result;
				var studentsAttendances = studentsCourses.Select(sc => new StudentsAttendances()
				{
					CourseId = entity.CourseId,
					DateTime = entity.DateTime,
					StudentId = sc.StudentId,
					Status = AttendanceStatus.Absent.ToString()
				});
				_context.StudentsAttendances.AddRange(studentsAttendances);
			}

			return Task.CompletedTask;
		}
	}
}
