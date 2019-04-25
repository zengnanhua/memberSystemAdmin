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
        /// <summary>
        /// 初始化种子数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public async Task SeedAsync(ApplicationDbContext context, IHostingEnvironment env)
        {
            if (!context.ApplicationUsers.Any())
            {
                ApplicationUser user = new ApplicationUser("admin", "管理员", "123456", phone: "15889421601");
                context.ApplicationUsers.Add(user);
                
                Role role = new Role("系统管理员", "系统管理员");
                context.Roles.Add(role);

                user.AddUserRole(role.Id);

                foreach (var userRole in user.UserRoleList)
                {
                    context.UserRoles.Add(userRole);
                }
                
               
                Menu menu = new Menu("base", "主菜单", "", "1", "", Domain.AggregatesModel.Common.PlatformType.Pc);
                Menu systemManage = menu.CreateSonMenu("systemManage", "系统管理",  "1", "",MenuFuntionType.Menu,PlatformType.Pc);
                Menu menuManage = systemManage.CreateSonMenu("menuManage", "菜单管理", "1", "", MenuFuntionType.Menu, PlatformType.Pc, "/permission/page");
                menuManage.SetMenuAttributeFeature(affix: true);
                Menu userManage = systemManage.CreateSonMenu("userManage", "用户管理", "2", "", MenuFuntionType.Menu, PlatformType.Pc, "/permission/directive");
                Menu roleManage = systemManage.CreateSonMenu("roleManage", "角色管理", "3", "", MenuFuntionType.Menu, PlatformType.Pc, "/permission/role");

                context.Menus.AddRange(menu, systemManage, menuManage, userManage, roleManage);


                role.AddPermission(menu.MenuNo, PlatformType.Pc);
                role.AddPermission(systemManage.MenuNo, PlatformType.Pc);
                role.AddPermission(menuManage.MenuNo, PlatformType.Pc);
                role.AddPermission(userManage.MenuNo, PlatformType.Pc);
                role.AddPermission(roleManage.MenuNo, PlatformType.Pc);

                foreach (var item in role.PermissionList)
                {
                    context.Permissions.Add(item);
                }

                await context.SaveChangesAsync();
            }
          
        }
    }
}
