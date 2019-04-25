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
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        private PageMenu CreatePageMenu(MenuDto entity)
        {
            PageMenu pageMenu = new PageMenu();
            pageMenu.children = new List<PageMenu>();
            pageMenu.name = entity.MenuNo;
            pageMenu.path = string.IsNullOrEmpty(entity.MenuUrl)?"": entity.MenuUrl;
            pageMenu.meta = new PageMenuMeta()
            {
                icon = string.IsNullOrWhiteSpace(entity.MenuIcon)? "dashboard" : entity.MenuIcon,
                title = entity.MenuName
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
                string sql = @"select DISTINCT * from Menus m inner join Permissions p
                    on m.MenuNo=p.MenuNo
                    where p.RoleId in (
		                    select r.Id from Roles r inner join  UserRoles ur on r.Id=ur.RoleId
		                    inner join ApplicationUsers u on ur.UserId=u.Id
		                    where u.id=@userId
                    )
                    or p.UserId=@userId";
                var result = (await connection.QueryAsync<MenuDto>(sql, new { userId })).ToList();
                var pageMenu= ConvertPageMenu(null,result,true);
                return pageMenu;
            }

        }
    }
}
