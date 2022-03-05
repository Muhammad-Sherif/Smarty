using Smarty.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarty.Data.Repositories.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<Course> Courses { get;}
		IGenericRepository<CourseGrade>	CourseGrades { get;}
		IGenericRepository<CourseAttendance> CourseAttendances { get; }
		IGenericRepository<StudentsGrades> StudentsGrades { get;}
		IGenericRepository<StudentsCourses> StudentsCourses { get;}
		IGenericRepository<Lab> Labs { get;}
		IGenericRepository<Member> Members { get;}
		int SaveChanges();
		Task<int> SaveChangesAsync();
	}
}

