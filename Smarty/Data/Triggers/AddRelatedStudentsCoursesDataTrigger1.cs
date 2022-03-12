using EntityFrameworkCore.Triggered;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;

namespace Smarty.Data.Triggers
{
	public class AddRelatedStudentsCoursesDataTrigger1 : IBeforeSaveTrigger<StudentsCourses>
	{
		public IUnitOfWork _context { get; set; }
		public AddRelatedStudentsCoursesDataTrigger1(IUnitOfWork context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<StudentsCourses> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added)
			{
				var entity = context.Entity;
				var coursesAttendances = _context.CourseAttendances.FindByCriteriaAsync(ca => ca.CourseId == entity.CourseId).Result;
				var StudentsAttendances = coursesAttendances
					.Select(ca => new StudentsAttendances() 
					{ 
						CourseId = ca.CourseId,
						DateTime= ca.DateTime,
						StudentId = entity.StudentId,
						Status= AttendanceStatus.Absent.ToString() 
					});

				_context.StudentsAttendances.AddRange(StudentsAttendances);
			}

			return Task.CompletedTask;
		}
	}
}
