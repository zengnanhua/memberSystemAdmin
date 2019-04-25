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
            //return _context.HttpContext.User.FindFirst("UserId").Value;
            return 1;
        }
    }
}
