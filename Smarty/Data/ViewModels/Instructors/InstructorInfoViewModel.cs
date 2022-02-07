using Smarty.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.ViewModels.Instructors
{
	public class InstructorInfoViewModel
	{

        [Required]
        [MaxLength(250)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(250)]
        public string LastName { get; set; }

        
        [Required]
        public Gender Gender { get; set; }

    }
}
