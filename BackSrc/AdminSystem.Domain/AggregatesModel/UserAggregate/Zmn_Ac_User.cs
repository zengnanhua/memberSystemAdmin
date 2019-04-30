using AdminSystem.Domain.CommonClass;
using AdminSystem.Domain.Events;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class Zmn_Ac_User : Entity, IAggregateRoot, IAudited
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
        /// 密码
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

        public List<Zmn_Ac_Permission> PermissionList { get; private set; }

        public List<Zmn_Ac_UserRole> UserRoleList { get; set; }


        public int UpdateUserId { get; private set; }

        public DateTime UpdateDateTime { get; private set; }


        public bool IsDelete { get; private set; }

        protected Zmn_Ac_User()
        {
            this.PermissionList = new List<Zmn_Ac_Permission>();
            this.UserRoleList = new List<Zmn_Ac_UserRole>();
            this.Address = new Address("", "", "", "", "");
        }
        public Zmn_Ac_User(string userName,string name,string pwd,string phone="",string sex="",string address=""):this()
        {
            this.UserName = userName;
            this.Name = name;
            this.Pwd = pwd;
            this.Sex = sex;
            this.Phone = phone;
            this.IsDelete = false;
            this.UpdateDateTime = DateTime.Now;
        }
        public void SetZmn_Ac_UserInfo( string name, string phone = "", string sex = "", string address = "")
        {
            this.Name = name;
            this.Sex = sex;
            this.Phone = phone;
            this.IsDelete = false;
            this.UpdateDateTime = DateTime.Now;
        }
        public void AddUserRole(int roleId)
        {
            this.UserRoleList.Add(new Zmn_Ac_UserRole(this.Id, roleId));
        }

        public void AddPermission(string menuNo, PlatformType platformType)
        {
            this.PermissionList.Add(new Zmn_Ac_Permission(this.Id,menuNo, PermissionType.UserPermission, platformType));
        }
        public void UpdateUser()
        {
            this.AddDomainEvent(new CreateUserChangeRoleDomainEvent(this.UserName));
        }

    }
}
