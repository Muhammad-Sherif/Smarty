using Microsoft.EntityFrameworkCore;
using Smarty.Data.Models;

namespace Smarty.Data.SmartyDBContext
{
    public class SmartyDbContext : DbContext
    {
        public SmartyDbContext(DbContextOptions<SmartyDbContext> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGrade> CourseGrades { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students{ get; set; }

    }
}
