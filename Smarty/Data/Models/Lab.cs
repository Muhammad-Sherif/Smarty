using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Models
{
    public class Lab
    {
        public string Name { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan Time { get; set; }
        public int CourseId { get; set; }



    }
}
