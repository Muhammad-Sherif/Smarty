using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smarty.Data.Models;

namespace Smarty.Data.Configurations.EntitiesConfigurations
{
    public class LabConfigurations : IEntityTypeConfiguration<Lab>
    {
        public void Configure(EntityTypeBuilder<Lab> builder)
        {
            EntityConfigurations(builder);
        }

        public void EntityConfigurations(EntityTypeBuilder<Lab> builder)
        {
            builder.HasKey(l => new { l.Name, l.CourseId });
            builder.Property(l => l.Name).IsRequired().HasMaxLength(250);
            builder.Property(l => l.Day).IsRequired();
            builder.Property(l => l.Time).IsRequired();


        }
        
    }
}
