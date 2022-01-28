using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smarty.Data.Models;

namespace Smarty.Data.Configurations.EntitiesConfigurations
{
    public class CourseGradeConfigurations : IEntityTypeConfiguration<CourseGrade>
    {
        public void Configure(EntityTypeBuilder<CourseGrade> builder)
        {
            EntityConfigurations(builder);
        }

        public void EntityConfigurations(EntityTypeBuilder<CourseGrade> builder)
        {
            builder.Property(cg => cg.Name).IsRequired().HasMaxLength(250);
            builder.Property(cg => cg.Grade).IsRequired();

        }

    }
}
