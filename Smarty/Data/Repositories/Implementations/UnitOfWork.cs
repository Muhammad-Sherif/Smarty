using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.SmartyDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarty.Data.Repositories.Implementations
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly SmartyDbContext _context;
		public IGenericRepository<Course> Courses { get; private set; }
		public IGenericRepository<CourseGrade> CourseGrades{ get; private set; }

		public IGenericRepository<Lab> Labs { get; private set; }

		public IGenericRepository<Member> Members { get; private set; }


		public UnitOfWork(SmartyDbContext context)
		{
			_context = context;
			Courses = new GenericRepository<Course>(_context);
			CourseGrades = new GenericRepository<CourseGrade>(_context);
			Labs = new GenericRepository<Lab>(_context);
			Members = new GenericRepository<Member>(_context);

		}
		public void Dispose()
		{
			_context.Dispose();
		}

		public int SaveChanges()
		{
			return _context.SaveChanges();
		}
		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		
	}
}
