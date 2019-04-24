using AdminSystem.Domain.AggregatesModel.Common;
using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.RoleAggregate
{
    public class Role:Entity,IAggregateRoot, IAudited
    {

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDescr { get; set; }

        public int CreateUserId { get; private set; }

        public DateTime CreateDateTime { get; private set; }

        public int UpdateUserId { get; private set; }

        public DateTime UpdateDateTime { get; private set; }

        public int DeleteUserId { get; private set; }

        public DateTime DeleteDateTime { get; private set; }

        public bool IsDelete { get; private set; }
        public List<Permission> PermissionList { get; private set; }

        protected Role()
        {
            this.PermissionList = new List<Permission>();
        }

        public Role(string roleName,string roleDescr):this()
        {
            this.RoleName = roleName;
            this.RoleDescr = roleDescr;
            this.CreateDateTime = DateTime.Now;
        }
        public void AddPermission(string menuNo, PlatformType platformType)
        {
            this.PermissionList.Add(new Permission(this.Id, menuNo, PermissionType.RolePermission, platformType));
        }
    }
}
