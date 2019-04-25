using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Services
{
    public interface IIdentityService
    {
        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <returns></returns>
        int GetUserId();
    }
}
