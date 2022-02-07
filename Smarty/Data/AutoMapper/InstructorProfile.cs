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
	public class InstructorProfile : Profile
	{
		public InstructorProfile()
		{
			CreateMap<InstructorInfoViewModel, Member>().As<Instructor>();
			CreateMap<InstructorInfoViewModel, Instructor>();
			CreateMap<InstructorRegisterFormViewModel, SmartyUser>()
					.ForMember(dest => dest.Member, options => options.MapFrom(src => src.InstructorInfo))
					.ForMember(dest => dest.UserName, options => options.MapFrom(src => src.Email));

		}
	}
}
