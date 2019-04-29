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
        Task<Zmn_Ac_User> GetUserById(int id);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="zmd_ac_user"></param>
        void Update(Zmn_Ac_User zmd_ac_user);
    }
}
