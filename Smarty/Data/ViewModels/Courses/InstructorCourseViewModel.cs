namespace Smarty.Data.ViewModels.Courses
{
	public class InstructorCourseViewModel
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public Guid RegisterCode { get; set; }
		public Guid AccessCode { get; set; }

		public string Description { get; set; }

        public string Name { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan Time { get; set; }

    }
}
