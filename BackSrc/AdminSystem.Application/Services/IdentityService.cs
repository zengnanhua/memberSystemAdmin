using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Services
{
    public class IdentityService: IIdentityService
    {
        private IHttpContextAccessor _context;
        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <returns></returns>
        public int GetUserId()
        {
            return Convert.ToInt32(_context.HttpContext.User.FindFirst("UserId").Value);
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            return _context.HttpContext.User.FindFirst("UserName").Value;
        }
        /// <summary>
        /// 获取角色id
        /// </summary>
        /// <returns></returns>
        public string GetRoleIds()
        {
            return _context.HttpContext.User.FindFirst("RoleIds").Value;
        }
    }
}
