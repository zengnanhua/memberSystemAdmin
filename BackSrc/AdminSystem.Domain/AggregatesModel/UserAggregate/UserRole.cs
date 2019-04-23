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
    public class UserRole : Entity
    {
        public int UserId { get; private set; }
        public ApplicationUser ApplicationUser{ get; private set; }
        public int RoleId { get; private set; }
        public Role Role { get; private set; }

        protected UserRole() { }

        public UserRole(int userId, int roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }
    }
}
