using AdminSystem.Domain.AggregatesModel.Common;
using AdminSystem.Domain.Events;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class ApplicationUser : Entity, IAggregateRoot, IAudited
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        ///  用户账号
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// 用户最后更新时间
        /// </summary>
        public string Pwd { get; private set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string Sex { get; private set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string Phone { get; private set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public Address Address { get; private set; }

        public List<Permission> PermissionList { get; private set; }

        public List<UserRole> UserRoleList { get; set; }

        public int CreateUserId { get; private set; }

        public DateTime CreateDateTime { get; private set; }

        public int UpdateUserId { get; private set; }

        public DateTime UpdateDateTime { get; private set; }

        public int DeleteUserId { get; private set; }

        public DateTime DeleteDateTime { get; private set; }

        public bool IsDelete { get; private set; }

        protected ApplicationUser()
        {
            this.PermissionList = new List<Permission>();
            this.UserRoleList = new List<UserRole>();
        }
        public ApplicationUser(string userName,string name,string pwd,string phone="",string sex="",string address=""):this()
        {
            this.UserName = userName;
            this.Name = name;
            this.Pwd = pwd;
            this.Sex = sex;
            this.IsDelete = false;
            this.CreateDateTime = DateTime.Now;
            this.Address = new Address("","","","","");
        }
        public void AddUserRole(int userId, int roleId)
        {
            this.UserRoleList.Add(new UserRole(userId, roleId));
        }

        public void AddPermission(int userIdOrRoleId, string menuNo, PermissionType permissionType, PlatformType platformType)
        {
            this.PermissionList.Add(new Permission(userIdOrRoleId,menuNo,permissionType,platformType));
        }
        public void UpdateUser()
        {
            this.AddDomainEvent(new CreateUserChangeRoleDomainEvent(this.UserName));
        }

    }
}
