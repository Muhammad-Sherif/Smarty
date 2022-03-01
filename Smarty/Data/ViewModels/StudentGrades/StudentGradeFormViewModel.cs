using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.ViewModels.StudentGrades
{
	public class StudentGradeFormViewModel
	{
		[Required]
		public int StudentId{ get; set; }
		[Required]
		public int CourseId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public double Value { get; set; }
	}
}
