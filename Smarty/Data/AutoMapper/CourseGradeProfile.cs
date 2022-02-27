using AutoMapper;
using Smarty.Data.Models;
using Smarty.Data.ViewModels.CourseGrades;
using Smarty.Data.ViewModels.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.AutoMapper
{
	public class CourseGradeProfile : Profile
	{
		public CourseGradeProfile()
		{
			CreateMap<CourseGrade, CourseGradeViewModel>();
			CreateMap<Course, SelectCourseViewModel>();
			CreateMap<CourseGradeFormViewModel, CourseGrade>().ReverseMap();

		}
	}
}
