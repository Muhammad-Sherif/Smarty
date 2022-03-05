using AutoMapper;
using Smarty.Data.Models;
using Smarty.Data.ViewModels.CourseAttendances;
using Smarty.Data.ViewModels.CourseGrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.AutoMapper
{
    public class CourseAttendanceProfile : Profile
    {
        public CourseAttendanceProfile()
        {
            CreateMap<CourseAttendanceFormViewModel, CourseAttendance>();
            CreateMap<CourseAttendance, CourseAttendanceViewModel>()
                .ForMember(dest => dest.UrlDateTime, options => options.MapFrom(src => src.DateTime.ToString("M-d-yyyy")))
                .ForMember(dest => dest.DisplayDateTime, options => options.MapFrom(src => src.DateTime.ToString("dddd, dd MMMM yyyy")));
        }

    }
}
