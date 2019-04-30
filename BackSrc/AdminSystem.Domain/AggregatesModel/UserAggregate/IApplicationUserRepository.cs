using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public interface IApplicationUserRepository:IRepository<Zmn_Ac_User>
    {
        void AddUser(Zmn_Ac_User user);
        /// <summary>
        /// 更加id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Zmn_Ac_User> GetUserByIdAsync(int id);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="zmd_ac_user"></param>
        void Update(Zmn_Ac_User zmd_ac_user);
        /// <summary>
        /// 根据手机号码查询用户
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Zmn_Ac_User GetUserByPhone(string phone);
        /// <summary>
        /// 根据用户名查询用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Zmn_Ac_User GetUserByUserName(string userName);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        void Delete(Zmn_Ac_User user);
    }
}
