﻿using AdminSystem.Infrastructure.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdminSystem.Application.Commands
{
    /// <summary>
    /// 获取用户Token
    /// </summary>
    public class GetTokenCommand : IRequest<ResultData<string>>
    {

        /// <summary>
        /// 用户名/手机号码
        /// </summary>
        public string IdentityName { get; private set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }

        public GetTokenCommand(string identityName, string password)
        {
            this.IdentityName = identityName;
            this.Password = password;
        }
    }
    /// <summary>
    /// 创建用户
    /// </summary>
    public class CreateUserCommand : IRequest<ResultData<string>>
    {
        public string Name { get; private set; }
        public string UserName { get; private set; }
        public string Sex { get; private set; }
        public string Phone { get; private set; }
        public CreateUserCommand(string name, string userName, string sex, string phone)
        {
            this.Name = name;
            this.UserName = userName;
            this.Sex = sex;
            this.Phone = phone;
        }
    }
    public class UpdateUserCommand : IRequest<ResultData<string>>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Sex { get; private set; }
        public string Phone { get; private set; }
        public UpdateUserCommand(int id, string name, string sex, string phone)
        {
            this.Id = id;
            this.Name = name;
            this.Sex = sex;
            this.Phone = phone;
        }
    }
    public class DeleteUserCommand : IRequest<ResultData<string>>
    {
        public int Id { get; private set; }
        public DeleteUserCommand(int id)
        {
            this.Id = id;
        }
    }
}