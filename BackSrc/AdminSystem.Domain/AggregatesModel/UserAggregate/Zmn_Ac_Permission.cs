using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.CommonClass;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class Zmn_Ac_Permission:Entity
    {
        public int? UserId { get; private set; }
        public Zmn_Ac_User ApplicationUser { get;private set; }
        public int? RoleId { get; private set; }
        public Zmn_Ac_Role Role { get; private set; }
        public string MenuNo { get; private set; }

        public PermissionType PermissionType { get; private set; }
        public PlatformType PlatformType { get; private set; }

        protected Zmn_Ac_Permission() { }

        public Zmn_Ac_Permission(int userIdOrRoleId,string menuNo,PermissionType permissionType,PlatformType platformType)
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
    [EnumRemark("权限种类")]
    public enum PermissionType
    {
        [EnumRemark("用户权限")]
        UserPermission=1,
        [EnumRemark("角色权限")]
        RolePermission=2,
    }

}
