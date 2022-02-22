using AutoMapper;
using Smarty.Data.Models;
using Smarty.Data.ViewModels.Courses;

namespace Smarty.Data.AutoMapper
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseViewModel, Course>()
                .ForMember(dest=>dest.RegisterCode , option=>option.MapFrom(o=>Guid.NewGuid()))
                .ForMember(dest=>dest.AccessCode, option =>option.MapFrom(o=> Guid.NewGuid()));


        }
    }
}
