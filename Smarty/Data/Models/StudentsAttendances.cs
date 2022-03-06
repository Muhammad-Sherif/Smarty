using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Models
{
	public class StudentsAttendances
	{
		public int CourseId { get; set; }
		public DateTime DateTime { get; set; }
		public CourseAttendance CourseAttendance { get; set; }
		public int StudentId { get; set; }
		public Student Student { get; set; }
		public string Remarks { get; set; }
		public string Status { get; set; }

	}
}
