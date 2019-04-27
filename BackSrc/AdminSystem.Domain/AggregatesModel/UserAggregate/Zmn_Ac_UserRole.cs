using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    /// <summary>
    /// 用户角色权限
    /// </summary>
    public class Zmn_Ac_UserRole : Entity
    {
        public int UserId { get; private set; }
        public Zmn_Ac_User ApplicationUser{ get; private set; }
        public int RoleId { get; private set; }
        public Zmn_Ac_Role Role { get; private set; }

        protected Zmn_Ac_UserRole() { }

        public Zmn_Ac_UserRole(int userId, int roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }
    }
}
