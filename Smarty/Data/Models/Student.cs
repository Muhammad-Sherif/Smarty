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
        public int Year{ get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<StudentsCourses> StudentsCourses{ get; set; }
        public ICollection<Lab> Labs{ get; set; }
        public ICollection<StudentsLabs> StudentsLabs { get; set; }


    }

}
