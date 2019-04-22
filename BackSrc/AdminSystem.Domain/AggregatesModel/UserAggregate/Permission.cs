using AdminSystem.Domain.AggregatesModel.Common;
using AdminSystem.Domain.AggregatesModel.CommonClass;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class Permission:Entity
    {
        public int UserId { get; private set; }
        public int RoleId { get; private set; }
        public string MenuNo { get; private set; }
        public PermissionType PermissionType { get; private set; }
        public PlatformType PlatformType { get; private set; }    
        
    }

    public enum PermissionType
    {
        [Remark("用户权限")]
        UserPermission=1,
        [Remark("角色权限")]
        RolePermission=2,
    }

}
