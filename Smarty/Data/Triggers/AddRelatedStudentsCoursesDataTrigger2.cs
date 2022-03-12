using EntityFrameworkCore.Triggered;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;

namespace Smarty.Data.Triggers
{
	public class AddRelatedStudentsCoursesDataTrigger2 : IBeforeSaveTrigger<StudentsCourses>
	{
		public IUnitOfWork _context { get; set; }
		public AddRelatedStudentsCoursesDataTrigger2(IUnitOfWork context)
		{
			_context = context;
		}

		public  Task BeforeSave(ITriggerContext<StudentsCourses> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added)
			{
				var entity = context.Entity;
				var coursesGrades = _context.CourseGrades.FindByCriteriaAsync(cg => cg.CourseId == entity.CourseId).Result;
				var StudentsGrades = coursesGrades.Select(cg => new StudentsGrades() 
				{ 
					CourseId = cg.CourseId,
					Name = cg.Name, 
					StudentId = entity.StudentId,
					Value = 0
				});
				_context.StudentsGrades.AddRange(StudentsGrades);
			}

			return Task.CompletedTask;
		}
	}
}
