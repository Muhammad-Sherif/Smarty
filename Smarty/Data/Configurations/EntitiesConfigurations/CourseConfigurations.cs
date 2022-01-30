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
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            EntityConfigurations(builder);
            RelationsConfigurations(builder);
        }

        public void EntityConfigurations(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(250);
            builder.Property(c => c.Description).IsRequired(false).HasMaxLength(2500);
            builder.Property(c => c.Day).IsRequired();
            builder.Property(c => c.Time).IsRequired();
            builder.Property(c => c.AccessCode).IsRequired();
            builder.Property(c => c.RegisterCode).IsRequired();

        }
        public void RelationsConfigurations(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasMany(c => c.Labs)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(c => c.CourseGrades)
                .WithOne(cg => cg.Course)
                .HasForeignKey(cg => cg.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


        }


    }
}
