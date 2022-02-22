using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smarty.Data.Models;

namespace Smarty.Data.Configurations.EntitiesConfigurations
{
    public class CourseGradeConfigurations : IEntityTypeConfiguration<CourseGrade>
    {
        public void Configure(EntityTypeBuilder<CourseGrade> builder)
        {
            RelationsConfigurations(builder);
        }

        public void RelationsConfigurations(EntityTypeBuilder<CourseGrade> builder)
        {
            builder.HasKey(cg => new { cg.Name,cg.CourseId});
            builder.Property(cg => cg.Name).IsRequired().HasMaxLength(250);
            builder.Property(cg => cg.MaxValue).IsRequired();
        }

    }
}
