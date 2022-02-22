using Microsoft.AspNetCore.Mvc;

namespace Smarty.Data.ViewModels.CourseGrades
{
	public class CourseGradeViewModel
	{
		public int CourseId { get; set; }
		public string Name { get; set; }
		public double Grade{ get; set; }

	}
}
