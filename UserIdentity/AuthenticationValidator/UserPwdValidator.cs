using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserIdentity.AuthenticationValidator
{
    public class UserPwdValidator : IExtensionGrantValidator
    {
        public string GrantType => ValidatorConst.UserPwd;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var identityName = context.Request.Raw["identityName"];
            var password = context.Request.Raw["password"];
            var dic = new Dictionary<string, object>();
            dic.Add("ResultCode","10004");
            dic.Add("ResultMessage","");
            dic.Add("data", "");
            var result = new GrantValidationResult(customResponse:dic);
            result.IsError = true;
            if (string.IsNullOrWhiteSpace(identityName) || string.IsNullOrWhiteSpace(password))
            {
                dic["ResultMessage"] = "用户名或密码不能为空";
                context.Result = result;
                return;
            }
            if (identityName == "admin" && password == "sa123")
            {
                var clims= new List<Claim>()
                {
                    new Claim("UserName", "admin"),
                     new Claim("Role", "admin")
                };
                result = new GrantValidationResult(identityName, GrantType,claims: clims, customResponse:dic);
                dic["ResultCode"] = "10000";
                dic["ResultMessage"] = "登录成功";

                context.Result = result;
                return;
            }
            dic["ResultMessage"] = "用户名或密码错误";
            context.Result = result;
            return;
        }
    }
}
