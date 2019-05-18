using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminSystem.Application.Commands;
using AdminSystem.Application.Queries;
using AdminSystem.Application.Services;
using AdminSystem.Infrastructure.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace AdminSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SysManageController
    {
        private ISysmanageQuery _sysmanageQuery { get; set; }
        public SysManageController(ISysmanageQuery sysmanageQuery)
        {
            this._sysmanageQuery = sysmanageQuery;
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<List<GetAttributeListResult>>> GetAttributeList(GetAttributeListParameter param)
        {
            var pageView = await _sysmanageQuery.GetAttributeList(param);
            return ResultData<List<GetAttributeListResult>>.CreateResultDataSuccess("成功", pageView.Data,pageView.Total);
        }
    }
}
