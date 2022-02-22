using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Models
{
	public class StudentsGrades
	{
		public int CourseId { get; set; }
		public string Name { get; set; }
		public CourseGrade CourseGrade { get; set; }

		public int StudentId { get; set; }
		public Student Student{ get; set; }

		public double Value { get; set; }


	}
}
