using AdminSystem.Domain.AggregatesModel.MenuAggregate;
using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Infrastructure.EntityConfigurations
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<Zmn_Ac_User>
    {
        public void Configure(EntityTypeBuilder<Zmn_Ac_User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.OwnsOne(o => o.Address);
            builder.HasIndex(c =>c.UserName).IsUnique();
            builder.HasIndex(c => c.Phone).IsUnique();
            builder.HasQueryFilter(c => !c.IsDelete);

        }
    }
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Zmn_Ac_Role>
    {
        public void Configure(EntityTypeBuilder<Zmn_Ac_Role> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<Zmn_Ac_UserRole>
    {
        public void Configure(EntityTypeBuilder<Zmn_Ac_UserRole> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(ur => ur.ApplicationUser).WithMany(u=>u.UserRoleList).HasForeignKey(pt=>pt.UserId);
            builder.HasOne(ur => ur.Role).WithMany().HasForeignKey(pr => pr.RoleId);
        }
    }
    public class MenuEntityTypeConfiguration : IEntityTypeConfiguration<Zmn_Ac_Menu>
    {
        public void Configure(EntityTypeBuilder<Zmn_Ac_Menu> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.MenuNo).IsUnique();
            builder.HasIndex(c => c.PMenuNo);
            builder.HasIndex(c => c.DeepPath);
        }
    }
    public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Zmn_Ac_Permission>
    {
        public void Configure(EntityTypeBuilder<Zmn_Ac_Permission> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.ApplicationUser).WithMany(u => u.PermissionList).HasForeignKey(c => c.UserId);
            builder.HasOne(c => c.Role).WithMany(u=>u.PermissionList).HasForeignKey(c => c.RoleId);
          
        }
    }


}
