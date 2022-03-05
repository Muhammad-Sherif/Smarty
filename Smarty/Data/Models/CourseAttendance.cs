namespace Smarty.Data.Models
{
    public class CourseAttendance
    {
        public int CourseId{ get; set; }
        public DateTime DateTime { get; set; }
        public bool QRCodeEnabled{ get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Course Course { get; set; }
    }
}
