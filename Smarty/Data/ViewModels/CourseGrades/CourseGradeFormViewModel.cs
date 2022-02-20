using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.ViewModels.CourseGrades
{
	public class CourseGradeFormViewModel
	{
		[Required]
		public int CourseId { get; set; }
		[Required]
		[StringLength(250)]
		public string Name { get; set; }
		[Required]
		public int Grade { get; set; }
	}
}
