using AdminSystem.Infrastructure.Common;
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
}
