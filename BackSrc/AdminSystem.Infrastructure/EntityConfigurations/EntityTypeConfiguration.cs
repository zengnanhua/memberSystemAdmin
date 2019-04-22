using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Infrastructure.EntityConfigurations
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(c => c.Id);
            builder.OwnsOne(o => o.Address);
            builder.HasIndex(c =>c.UserName).IsUnique();
            builder.HasIndex(c => c.Phone).IsUnique();

        }
    }
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(t => new { t.UserId, t.RoleId });

            builder.HasOne(ur => ur.ApplicationUser).WithMany(u=>u.UserRoleList).HasForeignKey(pt=>pt.UserId);
            builder.HasOne(ur => ur.Role).WithMany().HasForeignKey(pr => pr.RoleId);
        }
    }
}
