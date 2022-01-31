using AutoMapper;
using Smarty.Data.Models;
using Smarty.Data.ViewModels.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateInstructorViewModel, Instructor>();
        }
    }
}
