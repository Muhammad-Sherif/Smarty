using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smarty.Pages.CourseAttendances
{
    public class LocationAccessErrorModel : PageModel
    {
		public LocationAccessErrorModel()
		{

		}
		public string ErrorMessage { get; set; }
		public void OnGet(int codeError)
        {
            if (codeError == 1)
            {
                ErrorMessage = "Please Enable access to location so that QRcode Attendance feature can work well !";
            }
            else if (codeError == 2)
            {
                ErrorMessage  = "Location information is not supported by this browser, QRCode Attendance feature will not work !";
            }
            else if (codeError == 3)
            {

                ErrorMessage = "timeout! faild to get Location information, please try again";
            }
            else
            {
                ErrorMessage = "An unknown error occurred, QRCode Attendance feature will not work !";
            }
        }
    }
}
