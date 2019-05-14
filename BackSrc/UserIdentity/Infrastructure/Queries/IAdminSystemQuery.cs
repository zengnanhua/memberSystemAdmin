using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserIdentity.Infrastructure.Queries
{
    public interface IAdminSystemQuery
    {
        /// <summary>
        /// 根据用户名或手机号码查询
        /// </summary>
        /// <param name="userNameOrPhone"></param>
        /// <returns></returns>
        UserDto GetUserByUserNameOrPhone(string userNameOrPhone);
        /// <summary>
        /// 获取用户角色id 中间用逗号拼接
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetRoleIdsByUserId(int userId);
    }
}
