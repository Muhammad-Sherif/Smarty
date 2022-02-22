using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Models
{
    public class CourseGrade
    {
        public string Name{ get; set; }
        public double Grade { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<StudentsGrades> StudentsGrades { get; set; }
        public ICollection<Student> Students { get; set; }


    }
}
