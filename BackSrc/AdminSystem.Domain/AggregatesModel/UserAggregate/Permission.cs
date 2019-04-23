using AdminSystem.Domain.AggregatesModel.Common;
using AdminSystem.Domain.AggregatesModel.CommonClass;
using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class Permission:Entity
    {
        public int UserId { get; private set; }
        public ApplicationUser ApplicationUser { get;private set; }
        public int RoleId { get; private set; }
        public Role Role { get; private set; }
        public string MenuNo { get; private set; }

        public PermissionType PermissionType { get; private set; }
        public PlatformType PlatformType { get; private set; }

        protected Permission() { }

        public Permission(int userIdOrRoleId,string menuNo,PermissionType permissionType,PlatformType platformType)
        {
            if (permissionType == PermissionType.UserPermission)
            {
                this.UserId = userIdOrRoleId;
            }
            else
            {
                this.RoleId = userIdOrRoleId;
            }
            this.MenuNo = menuNo;
            this.PermissionType = permissionType;
            this.PlatformType = platformType;
        }

    }

    public enum PermissionType
    {
        [Remark("用户权限")]
        UserPermission=1,
        [Remark("角色权限")]
        RolePermission=2,
    }

}
