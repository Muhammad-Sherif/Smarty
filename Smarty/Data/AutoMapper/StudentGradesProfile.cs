using AutoMapper;
using Smarty.Data.Models;
using Smarty.Data.ViewModels.StudentGrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.AutoMapper
{
	public class StudentGradesProfile : Profile
	{
		public StudentGradesProfile()
		{
			CreateMap<StudentsGrades, StudentGradeViewModel>()
				.ForMember(dest => dest.MaxValue, src => src.MapFrom(src => src.CourseGrade.MaxValue));
			CreateMap<StudentsGrades, StudentGradeFormViewModel>().ReverseMap();

		}
	}
}
