using System.ComponentModel.DataAnnotations;

namespace Smarty.Data.ViewModels.CourseAttendances
{
	public class CourseAttendanceEditFormViewModel
	{
        [Required]
        public int CourseId { get; set; }
        [Required]
        public double AcceptedScanDistance { get; set; }
        [Required]
        public bool QRCodeEnabled { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

    }
}
