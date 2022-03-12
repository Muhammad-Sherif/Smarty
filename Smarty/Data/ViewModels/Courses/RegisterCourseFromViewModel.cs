using System.ComponentModel.DataAnnotations;

namespace Smarty.Data.ViewModels.Courses
{
	public class RegisterCourseFromViewModel
	{
		[Required]
		public Guid RegisterCode { get; set; }

	}
}
