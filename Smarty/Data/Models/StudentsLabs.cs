using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Models
{
    public class StudentsLabs
    {

        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string LabName{ get; set; }
        public int CourseId { get; set; }
        public Lab Lab { get; set; }

    }
}
