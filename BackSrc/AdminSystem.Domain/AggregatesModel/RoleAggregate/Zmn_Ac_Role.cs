using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.CommonClass;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.RoleAggregate
{
    public class Zmn_Ac_Role:Entity,IAggregateRoot, IAudited
    {

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDescr { get; set; }

        public int UpdateUserId { get; private set; }

        public DateTime UpdateDateTime { get; private set; }

        public bool IsDelete { get; private set; }
        public List<Zmn_Ac_Permission> PermissionList { get; private set; }

        protected Zmn_Ac_Role()
        {
            this.PermissionList = new List<Zmn_Ac_Permission>();
        }

        public Zmn_Ac_Role(string roleName,string roleDescr):this()
        {
            this.RoleName = roleName;
            this.RoleDescr = roleDescr;
            this.UpdateDateTime = DateTime.Now;
        }
        public void AddPermission(string menuNo, PlatformType platformType)
        {
            this.PermissionList.Add(new Zmn_Ac_Permission(this.Id, menuNo, PermissionType.RolePermission, platformType));
        }
    }
}
