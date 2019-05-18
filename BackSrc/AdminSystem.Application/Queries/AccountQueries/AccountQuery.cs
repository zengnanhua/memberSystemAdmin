using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSystem.Application.Queries
{
    public class AccountQuery: IAccountQuery
    {
        private string _connectionString = string.Empty;
        public AccountQuery(string constr)
        {
           // Microsoft.AspNetCore.SignalR.Hub
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        #region 获取页面菜单
        private PageMenu CreatePageMenu(MenuDto entity)
        {
            PageMenu pageMenu = new PageMenu();
            pageMenu.children = new List<PageMenu>();
            pageMenu.alwaysShow = entity.AlwaysShow;
            pageMenu.name = entity.MenuNo;
            pageMenu.path = string.IsNullOrEmpty(entity.MenuUrl)?"": entity.MenuUrl;
            pageMenu.meta = new PageMenuMeta()
            {
                icon = string.IsNullOrWhiteSpace(entity.MenuIcon) ? "dashboard" : entity.MenuIcon,
                title = entity.MenuName,
                affix = entity.Affix,
                noCache=entity.NoCache
            };
            return pageMenu;
        }
        private PageMenu ConvertPageMenu(MenuDto baseMenuDto, List<MenuDto> menuDtoList, bool first)
        {

            //是否你第一次进来
            PageMenu pageMenu = null;
            if (first)
            {
                baseMenuDto = menuDtoList.Where(c => c.MenuNo == "base").FirstOrDefault();
                if (baseMenuDto == null)
                {
                    return null;
                }
            }
            pageMenu = CreatePageMenu(baseMenuDto);

            var list = menuDtoList.Where(c => c.PMenuNo == baseMenuDto.MenuNo&&c.MenuNo!=baseMenuDto.MenuNo).ToList();
            if (list == null || list.Count <= 0)
            {
                return pageMenu;
            }
            foreach (var temp in list)
            {
                var a = ConvertPageMenu(temp, menuDtoList, false);
                if (a != null)
                {
                    pageMenu.children.Add(a);
                }
            }
            return pageMenu;
        }
        /// <summary>
        /// 获取页面菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<PageMenu> GetPageMenuByUserId(int userId)
        {

            using (var connection = new MySqlConnection(_connectionString))
            {
                string sql = @"select DISTINCT * from Zmn_Ac_Menus m inner join Zmn_Ac_Permissions p
                    on m.MenuNo=p.MenuNo
                    where p.RoleId in (
		                    select r.Id from Zmn_Ac_Roles r inner join  Zmn_Ac_UserRoles ur on r.Id=ur.RoleId
		                    inner join Zmn_Ac_Users u on ur.UserId=u.Id
		                    where u.id=@userId
                    )
                    or p.UserId=@userId";
                var result = (await connection.QueryAsync<MenuDto>(sql, new { userId })).ToList();
                var pageMenu= ConvertPageMenu(null,result,true);
                return pageMenu;
            }

        }
        #endregion

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PageView<UserDto>> GetUserList(GetUserListParameter param)
        {

            var whereSql = "";
            if (!string.IsNullOrWhiteSpace(param.Name))
            {
                whereSql += " and name=@name";
            }
            if (!string.IsNullOrWhiteSpace(param.Phone))
            {
                whereSql += " and Phone=@phone";
            }
            string sql = $@"								 select a.id, a.UserName,a.Name,a.Phone,a.Sex,a.UpdateDateTime
                        ,GROUP_CONCAT(c.RoleName SEPARATOR ';') roleAll  from Zmn_Ac_Users a INNER JOIN  Zmn_Ac_UserRoles b
                        on a.Id=b.UserId
                        INNER JOIN Zmn_Ac_Roles c 
                        on c.Id=b.RoleId 
						where 1=1 and a.isDelete=0  {whereSql}
                        GROUP BY a.id,a.UserName,a.Name,a.Phone,a.Sex,a.UpdateDateTime
						order by a.Id desc ";

            return await PaginationHelp.GetPageDataAsync<UserDto>(sql, param, _connectionString);
        }
        #region 获取菜单Tree
        private static MenuTree AutoCopy(MenuDto parent)
        {
            MenuTree child = new MenuTree();

            child.Children = new List<MenuTree>();

            var ParentType = typeof(MenuDto);

            var Properties = ParentType.GetProperties();

            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }

            return child;
        }

        private MenuTree ConvertMenuTree(MenuDto baseMenuDto, List<MenuDto> menuDtoList, bool first)
        {

            //是否你第一次进来
            MenuTree pageMenu = null;
            if (first)
            {
                baseMenuDto = menuDtoList.Where(c => c.MenuNo == "base").FirstOrDefault();
                if (baseMenuDto == null)
                {
                    return null;
                }
            }
           pageMenu = AutoCopy(baseMenuDto);

            var list = menuDtoList.Where(c => c.PMenuNo == baseMenuDto.MenuNo && c.MenuNo != baseMenuDto.MenuNo).ToList();
            if (list == null || list.Count <= 0)
            {
                return pageMenu;
            }
            foreach (var temp in list)
            {
                var a = ConvertMenuTree(temp, menuDtoList, false);
                if (a != null)
                {
                    pageMenu.Children.Add(a);
                }
            }
            return pageMenu;
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public async Task<MenuTree> GetMenuTreeAsync()
        {
            string sql = "select * from Zmn_Ac_Menus";
            using (var connection = new MySqlConnection(_connectionString))
            {
                var list = (await connection.QueryAsync<MenuDto>(sql)).ToList();
                var menuTree= ConvertMenuTree(null, list, true);
                return menuTree;
            }
        }
        #endregion


    }
}
