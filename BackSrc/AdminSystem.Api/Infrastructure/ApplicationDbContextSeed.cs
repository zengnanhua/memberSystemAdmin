using AdminSystem.Domain.AggregatesModel.AttributeAggregate;
using AdminSystem.Domain.AggregatesModel.MenuAggregate;
using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.CommonClass;
using AdminSystem.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            #region 1.权限用户角色等数据
            if (!context.Zmn_Ac_Users.Any())
            {
                Zmn_Ac_User user = new Zmn_Ac_User("admin", "管理员", "123456", phone: "15889421601");
                context.Zmn_Ac_Users.Add(user);
                
                Zmn_Ac_Role role = new Zmn_Ac_Role("系统管理员", "系统管理员");
                context.Zmn_Ac_Roles.Add(role);

                user.AddUserRole(role.Id);

                foreach (var userRole in user.UserRoleList)
                {
                    context.Zmn_Ac_UserRoles.Add(userRole);
                }
                
               
                Zmn_Ac_Menu menu = new Zmn_Ac_Menu("base", "主菜单", "", "1", "", PlatformType.Pc);
                Zmn_Ac_Menu systemManage = menu.CreateSonMenu("systemManage", "系统管理",  "1", "",MenuFuntionType.Menu,PlatformType.Pc);
                Zmn_Ac_Menu menuManage = systemManage.CreateSonMenu("menuManage", "菜单管理", "1", "", MenuFuntionType.Menu, PlatformType.Pc, "/systemManage/menuManage");
                menuManage.SetMenuAttributeFeature(affix: true);
                Zmn_Ac_Menu userManage = systemManage.CreateSonMenu("userManage", "用户管理", "2", "", MenuFuntionType.Menu, PlatformType.Pc, "/systemManage/userManage");
                Zmn_Ac_Menu roleManage = systemManage.CreateSonMenu("roleManage", "角色管理", "3", "", MenuFuntionType.Menu, PlatformType.Pc, "/systemManage/roleManage");

                context.Zmn_Ac_Menus.AddRange(menu, systemManage, menuManage, userManage, roleManage);


                role.AddPermission(menu.MenuNo, PlatformType.Pc);
                role.AddPermission(systemManage.MenuNo, PlatformType.Pc);
                role.AddPermission(menuManage.MenuNo, PlatformType.Pc);
                role.AddPermission(userManage.MenuNo, PlatformType.Pc);
                role.AddPermission(roleManage.MenuNo, PlatformType.Pc);

                foreach (var item in role.PermissionList)
                {
                    context.Zmn_Ac_Permissions.Add(item);
                }

                await context.SaveChangesAsync();
            }
            #endregion

            #region 2.系统枚举初始化到数据库
            {
                var oldList = context.Zmn_Sys_Attributes.Include(c=>c.DetailList).ToList();
                if (oldList != null&&oldList.Count>0)
                {
                    context.Zmn_Sys_Attributes.RemoveRange(oldList);
                }
               
                var enumList = typeof(PlatformType).GetTypeInfo().Assembly.GetTypes().Where(c => c.GetCustomAttribute(typeof(EnumRemarkAttribute)) != null && c.IsEnum).ToList();
                foreach (var item in enumList)
                {
                    var headAttribute = item.GetCustomAttribute<EnumRemarkAttribute>();
                    Zmn_Sys_Attribute zmn_Sys_Attribute = new Zmn_Sys_Attribute(item.Name,headAttribute.Remark);

                    var fields = item.GetEnumNames();
                    foreach (var fieldItem in fields)
                    {
                        var fieldInfo = item.GetField(fieldItem);
                        var bodyAttribute = fieldInfo.GetCustomAttribute<EnumRemarkAttribute>();
                        if (bodyAttribute == null)
                        {
                            throw new Exception($"{headAttribute.Remark}中的值‘{fieldItem}’没有加属性标签");
                        }
                        zmn_Sys_Attribute.AddAttributeDetail(Convert.ToInt32(fieldInfo.GetValue(null)).ToString(), bodyAttribute.Remark);

                    }

                    context.Zmn_Sys_Attributes.Add(zmn_Sys_Attribute);

                }
                await context.SaveChangesAsync();
            }
            #endregion
        }
    }
}
