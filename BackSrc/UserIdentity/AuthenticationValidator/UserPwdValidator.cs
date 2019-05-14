using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserIdentity.Infrastructure.Queries;

namespace UserIdentity.AuthenticationValidator
{
    public class UserPwdValidator : IExtensionGrantValidator
    {
        public string GrantType => ValidatorConst.UserPwd;
        private IAdminSystemQuery _adminSystemQuery;
        public UserPwdValidator(IAdminSystemQuery adminSystemQuery)
        {
            this._adminSystemQuery = adminSystemQuery;
        }
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var identityName = context.Request.Raw["identityName"];
            var password = context.Request.Raw["password"];
            var dic = new Dictionary<string, object>();
            dic.Add("resultCode", "10004"); //未授权
            dic.Add("resultMessage", "");
            dic.Add("data", "");
            var result = new GrantValidationResult(customResponse:dic);
            result.IsError = true;
            if (string.IsNullOrWhiteSpace(identityName) || string.IsNullOrWhiteSpace(password))
            {
                dic["resultMessage"] = "用户名或密码不能为空";
                context.Result = result;
                return;
            }
            var user= _adminSystemQuery.GetUserByUserNameOrPhone(identityName);
            if (user==null)
            {
                dic["resultMessage"] = "此用户不对";
                context.Result = result;
                return;
            }
            if (password==user.Pwd)
            {
                var clims= new List<Claim>()
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("RoleIds",_adminSystemQuery.GetRoleIdsByUserId(user.Id))
                };
                result = new GrantValidationResult(identityName, GrantType,claims: clims, customResponse:dic);
                dic["resultCode"] = "10000";
                dic["resultMessage"] = "登录成功";

                context.Result = result;
                return;
            }
            dic["resultMessage"] = "用户名或密码错误";
            context.Result = result;
            return;
        }
    }
}
