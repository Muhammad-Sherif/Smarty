using EntityFrameworkCore.Triggered;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;

namespace Smarty.Data.Triggers
{
	public class AddRelatedCourseGradeDataTrigger : IBeforeSaveTrigger<CourseGrade>
	{
		public IUnitOfWork _context { get; set; }
		public AddRelatedCourseGradeDataTrigger(IUnitOfWork context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<CourseGrade> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added)
			{
				var entity = context.Entity;
				var studentsCourses = _context.StudentsCourses.FindByCriteriaAsync(sc => sc.CourseId == entity.CourseId).Result;
				var studentsGrades = studentsCourses.Select(sc => new StudentsGrades()
				{
					CourseId = entity.CourseId,
					Name = entity.Name,
					StudentId = sc.StudentId,
					Value = 0
				});
				_context.StudentsGrades.AddRange(studentsGrades);
			}

			return Task.CompletedTask;
		}
	}
}
