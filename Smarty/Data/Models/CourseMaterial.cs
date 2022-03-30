namespace Smarty.Data.Models
{
    public class CourseMaterial
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
