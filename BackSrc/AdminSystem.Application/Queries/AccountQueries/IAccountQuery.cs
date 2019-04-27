using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminSystem.Application.Queries
{
    public interface IAccountQuery
    {
        /// <summary>
        /// 获取页面菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<PageMenu> GetPageMenuByUserId(int userId);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<UserDto>> GetUserList(GetUserListParameter param);
    }
}
