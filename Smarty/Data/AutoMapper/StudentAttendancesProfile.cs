using AutoMapper;
using Smarty.Data.Models;
using Smarty.Data.ViewModels.StudentAttendances;

namespace Smarty.Data.AutoMapper
{
	public class StudentAttendancesProfile : Profile
	{
		public StudentAttendancesProfile()
		{
			CreateMap<StudentsAttendances, StudentAttendanceFormViewModel>().ReverseMap();
			CreateMap<StudentsAttendances, StudentAttendanceViewModel>()
				.ForMember(dest => dest.UrlDateTime, options => options.MapFrom(src => src.DateTime.ToString("M-d-yyyy")))
				.ForMember(dest => dest.DisplayDateTime, options => options.MapFrom(src => src.DateTime.ToString("dddd, dd MMMM yyyy")));
		}
	}
}
