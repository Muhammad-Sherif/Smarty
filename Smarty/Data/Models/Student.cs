using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Models
{
    public class Student : Member
    {
        public string Department { get; set; }
        public string UniversityYear{ get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<StudentsCourses> StudentsCourses{ get; set; }
        public ICollection<Lab> Labs{ get; set; }
        public ICollection<StudentsLabs> StudentsLabs { get; set; }
        public ICollection<CourseGrade> CoursesGrades{ get; set; }
        public ICollection<StudentsGrades> StudentsGrades { get; set; }
        public ICollection<StudentsAttendances> StudentsAttendances { get; set; }
        public ICollection<CourseAttendance> CoursesAttendances { get; set; }


    }

}
