namespace Smarty.Data.ViewModels.CourseAttendances
{
    public class CourseAttendanceViewModel
    {
        public int CourseId { get; set; }
        public string DisplayDateTime { get; set; }
        public string UrlDateTime { get; set; }
        public bool QRCodeEnabled { get; set; }

    }
}
