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

        protected Role() { }

        public Role(string roleName,string roleDescr)
        {
            this.RoleName = roleName;
            this.RoleDescr = RoleDescr;
            this.CreateDateTime = DateTime.Now;
        }
    }
}
