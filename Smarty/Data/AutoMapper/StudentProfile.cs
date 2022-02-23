using AutoMapper;
using Smarty.Data.Models;
using Smarty.Data.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.AutoMapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentInfoViewModel, Member>().As<Student>();
            CreateMap<StudentInfoViewModel, Student>();
            CreateMap<StudentRegisterFormViewModel, SmartyUser>()
                .ForMember(dest => dest.Member, options => options.MapFrom(src => src.StudentInfo))
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.Email));

            CreateMap<Student, SelectStudentViewModel>();


        }

    }
}
