using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smarty.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Data.Configurations.EntitiesConfigurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            EntityConfigurations(builder);
            RelationsConfigurations(builder);   
        }
        public void EntityConfigurations(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.Department).HasMaxLength(250);
            builder.Property(s => s.UniversityYear).HasMaxLength(250);
        }
        public void RelationsConfigurations(EntityTypeBuilder<Student> builder)
        {

			builder
				.HasMany(s => s.Courses)
				.WithMany(c => c.Students)
				.UsingEntity<StudentsCourses>(
					j => j
					.HasOne(sc => sc.Course)
					.WithMany(c => c.StudentsCourses)
                    .HasForeignKey(sc => sc.CourseId)
                    .OnDelete(DeleteBehavior.Cascade),
                    j => j
                    .HasOne(sc => sc.Student)
                    .WithMany(s => s.StudentsCourses)
                    .HasForeignKey(sc => sc.StudentId)
                    .OnDelete(DeleteBehavior.Restrict),
                    j => j
                    .HasKey(t => new { t.StudentId, t.CourseId})
                    );


            builder
                .HasMany(s => s.Labs)
                .WithMany(l => l.Students)
                .UsingEntity<StudentsLabs>(
                    j => j
                    .HasOne(sl => sl.Lab)
                    .WithMany(l => l.StudentsLabs)
                    .HasForeignKey(sl =>  sl.LabId)
                    .OnDelete(DeleteBehavior.Cascade),
                    j => j
                    .HasOne(sl => sl.Student)
                    .WithMany(s => s.StudentsLabs)
                    .HasForeignKey(sl => sl.StudentId)
                    .OnDelete(DeleteBehavior.Restrict),
                    j => j
                    .HasKey(t => new { t.StudentId , t.LabId, })
                    );

            builder
                .HasMany(s => s.CoursesGrades)
                .WithMany(cg => cg.Students)
                .UsingEntity<StudentsGrades>(
                    j => j
                    .HasOne(sg => sg.CourseGrade)
                    .WithMany(cg => cg.StudentsGrades)
                    .HasForeignKey(sg=> new { sg.Name , sg.CourseId})
                    .OnDelete(DeleteBehavior.Cascade),
					j => j
					.HasOne(sg => sg.Student)
					.WithMany(s => s.StudentsGrades)
					.HasForeignKey(sg => sg.StudentId)
					.OnDelete(DeleteBehavior.Restrict),
					j => j
					.HasKey(t => new { t.StudentId, t.CourseId, t.Name })
					);


		}


    }
}
