using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Models
{
    public class Instructor : Member
    {
        public ICollection<Course> Courses { get; set; }


    }
}
