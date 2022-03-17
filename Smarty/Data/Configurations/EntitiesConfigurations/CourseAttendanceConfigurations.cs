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
    public class CourseAttendanceConfigurations : IEntityTypeConfiguration<CourseAttendance>
    {
        public void Configure(EntityTypeBuilder<CourseAttendance> builder)
        {
            EntityConfigurations(builder);
        }
        void EntityConfigurations(EntityTypeBuilder<CourseAttendance> builder)
        {
            builder.HasKey(ca => new { ca.DateTime, ca.CourseId});
            builder.Property(ca => ca.QRCodeEnabled).HasDefaultValue(1);
            builder.Property(ca => ca.AcceptedScanDistance).IsRequired();
        }
        
    }
}
