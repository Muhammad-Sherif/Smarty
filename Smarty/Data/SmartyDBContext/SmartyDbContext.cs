using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smarty.Data.Models;
using System.Reflection;

namespace Smarty.Data.SmartyDBContext
{
    public class SmartyDbContext : IdentityDbContext<SmartyUser>
    {
        public SmartyDbContext(DbContextOptions<SmartyDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
		public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGrade> CoursesGrades { get; set; }
        public DbSet<StudentsGrades> StudentsGrades { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students{ get; set; }

    }
}
