namespace Smarty.Data.ViewModels.StudentAttendances
{
	public class StudentAttendanceViewModel
	{
		public int StudentId { get; set; }
		public int CourseId { get; set; }
		public string DisplayDateTime { get; set; }
		public string UrlDateTime { get; set; }
		public string Remarks { get; set; }
		public string Status{ get; set; }

	}
}
