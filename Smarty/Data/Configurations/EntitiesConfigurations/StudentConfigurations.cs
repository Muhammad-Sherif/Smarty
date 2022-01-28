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
        }
        public void EntityConfigurations(EntityTypeBuilder<Student> builder)
        {
            builder.Property(c => c.Department).HasMaxLength(250);

        }

    }
}
