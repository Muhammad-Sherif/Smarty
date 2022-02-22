using System.ComponentModel.DataAnnotations;

namespace Smarty.Data.ViewModels.Courses
{
    public class CourseViewModel
    {
        [Required]
        [StringLength(250)]
        public string Code { get; set; }

        [StringLength(2500)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        public TimeSpan Time { get; set; }
    }
}
