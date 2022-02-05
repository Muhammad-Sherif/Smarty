using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smarty.Data.Models;

namespace Smarty.Data.Configurations.EntitiesConfigurations
{
    public class MemberConfigurations : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            EntityConfigurations(builder);
            RelationsConfigurations(builder);   
        }

        void EntityConfigurations(EntityTypeBuilder<Member> builder)
        {
            builder.HasDiscriminator<string>("MemberType");
            builder.Property("MemberType").HasMaxLength(250);
            builder.Property(m=>m.SmartyUserId).IsRequired().HasMaxLength(450);
            builder.Property(m=>m.FirstName).IsRequired().HasMaxLength(250);
            builder.Property(m=>m.LastName).IsRequired().HasMaxLength(250);
            builder.Property(m=>m.Gender).IsRequired().HasMaxLength(250);
        }
        void RelationsConfigurations(EntityTypeBuilder<Member> builder)
		{
            builder
                .HasOne(m => m.SmartyUser)
                .WithOne(su => su.Member)
                .HasForeignKey<Member>(m => m.SmartyUserId)
                .OnDelete(DeleteBehavior.Cascade);

		}



    }
}
