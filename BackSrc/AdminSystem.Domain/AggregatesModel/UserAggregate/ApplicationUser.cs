using AdminSystem.Domain.Events;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class ApplicationUser: Entity, IAggregateRoot
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get;private set; }
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
        /// <summary>
        /// 最后更新人
        /// </summary>
        public int LastUpdateUserId { get;private set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string LastUpdateTime { get; private set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public string LastLoginTime { get; private set; }

        protected ApplicationUser()
        {
           
        }
        public ApplicationUser(string userName,string name,string pwd,string phone="",string sex="",string address="")
        {
            this.UserName = userName;
            this.Name = name;
            this.Pwd = pwd;
            this.Sex = sex;
            this.Address = new Address("55","44","33","22","1");
        }

        public void UpdateUser()
        {
            this.AddDomainEvent(new CreateUserChangeRoleDomainEvent(this.UserName));
        }

    }
}
