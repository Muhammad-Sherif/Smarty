namespace Smarty.Data.ViewModels.StudentGrades
{
	public class StudentGradeViewModel
	{
        public int StudentId { get; set; }
		public int CourseId{ get; set; }
		public string Name { get; set; }
		public double Value { get; set; }
		public double MaxValue { get; set; }
	}
}
