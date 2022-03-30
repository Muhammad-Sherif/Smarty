using System.ComponentModel.DataAnnotations;

namespace Smarty.Data.ViewModels.CourseMaterialViewModel
{
    public class UploadViewModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
