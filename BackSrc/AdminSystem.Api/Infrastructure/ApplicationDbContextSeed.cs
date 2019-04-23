using AdminSystem.Domain.AggregatesModel.Common;
using AdminSystem.Domain.AggregatesModel.MenuAggregate;
using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminSystem.Api.Infrastructure
{
    public class ApplicationDbContextSeed
    {
        public async Task SeedAsync(ApplicationDbContext context, IHostingEnvironment env)
        {
            if (!context.ApplicationUsers.Any())
            {
                ApplicationUser user = new ApplicationUser("admin", "管理员", "123456", phone: "15889421601");
                context.ApplicationUsers.Add(user);
                
                Role role = new Role("系统管理员", "系统管理员");
                context.Roles.Add(role);
              
                UserRole userRole = new UserRole(user.Id, role.Id);
                context.UserRoles.Add(userRole);
               
                Menu menu = new Menu("base", "主菜单", "", "1", "", Domain.AggregatesModel.Common.PlatformType.Pc);
                Menu systemManage = menu.CreateSonMenu("systemManage", "系统管理",  "1", "",MenuFuntionType.Menu,PlatformType.Pc);
                Menu menuManage = systemManage.CreateSonMenu("menuManage", "菜单管理", "1", "", MenuFuntionType.Menu, PlatformType.Pc, "/systemManage/menuManage");
                Menu userManage = systemManage.CreateSonMenu("userManage", "用户管理", "2", "", MenuFuntionType.Menu, PlatformType.Pc, "/systemManage/userManage");
                Menu roleManage = systemManage.CreateSonMenu("roleManage", "角色管理", "3", "", MenuFuntionType.Menu, PlatformType.Pc, "/systemManage/roleManage");

                context.Menus.AddRange(menu, systemManage, menuManage, userManage, roleManage);

                

                Permission menuPermission = new Permission(role.Id, menu.MenuNo, PermissionType.RolePermission,PlatformType.Pc);
                Permission systemManagePermission = new Permission(role.Id, systemManage.MenuNo, PermissionType.RolePermission, PlatformType.Pc);
                Permission menuManagePermission = new Permission(role.Id, menuManage.MenuNo, PermissionType.RolePermission, PlatformType.Pc);
                Permission userManagePermission = new Permission(role.Id, userManage.MenuNo, PermissionType.RolePermission, PlatformType.Pc);
                Permission roleManagePermission = new Permission(role.Id, roleManage.MenuNo, PermissionType.RolePermission, PlatformType.Pc);

                context.Permissions.AddRange(menuPermission, systemManagePermission, menuManagePermission, userManagePermission, roleManagePermission);

                await context.SaveChangesAsync();
            }
          
        }
    }
}
