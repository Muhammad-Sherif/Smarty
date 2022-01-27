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

    }

}
