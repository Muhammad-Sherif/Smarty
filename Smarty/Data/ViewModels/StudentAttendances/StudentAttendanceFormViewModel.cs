using Smarty.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Smarty.Data.ViewModels.StudentAttendances
{
	public class StudentAttendanceFormViewModel
	{
		[Required]
		public int StudentId{ get; set; }
		[Required]
		public int CourseId { get; set; }
		[Required]
		public DateTime DateTime { get; set; }
		public string Remarks { get; set; }
		[Required]
		public AttendanceStatus Status { get; set; }
	}
}
