using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace UserIdentity.Infrastructure.Queries
{
    public class AdminSystemQuery: IAdminSystemQuery
    {
        private string _connectionString = string.Empty;
        public AdminSystemQuery(IConfiguration Configuration)
        {
            _connectionString = Configuration.GetConnectionString("AdminSystemConnection");
        }
        /// <summary>
        /// 根据用户名或手机号码查询
        /// </summary>
        /// <param name="userNameOrPhone"></param>
        /// <returns></returns>
        public UserDto GetUserByUserNameOrPhone(string userNameOrPhone)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string sql = "select * from Zmn_Ac_Users where IsDelete=0 and ( UserName=@UserName or Phone=@Phone)";
                var user= connection.Query<UserDto>(sql, new { UserName = userNameOrPhone, Phone = userNameOrPhone }).FirstOrDefault();
                return user;
            }
        }
        /// <summary>
        /// 获取用户角色id 中间用逗号拼接
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetRoleIdsByUserId(int userId)
        {
            string sql = "select Roleid from Zmn_Ac_UserRoles where UserId=@UserId";
            using (var connection = new MySqlConnection(_connectionString))
            {
                var roleList = connection.Query<string>(sql, new { UserId = userId }).ToList();
                var str = "";
                for (var i = 0; i < roleList.Count; i++)
                {
                    if (i == 0)
                    {
                        str += roleList[i];
                    }
                    else
                    {
                        str += "," + roleList[i];
                    }
                }

                return str;
            }
        }
    }
}
