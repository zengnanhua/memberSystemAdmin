using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Infrastructure.EntityConfigurations
{
    public class ModelCreateConfiguration
    {
        public static void InitApplyConfiguration(ModelBuilder builder)
        {
            //用户配置
            builder.ApplyConfiguration(new ApplicationUserEntityTypeConfiguration());
            builder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            builder.ApplyConfiguration(new MenuEntityTypeConfiguration());
            builder.ApplyConfiguration(new PermissionEntityTypeConfiguration());
        }
    }
}
