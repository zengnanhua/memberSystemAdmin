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
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        IMediator _mediator;
        IAccountQuery _accountQuery;
        IIdentityService _identityService;
        public AccountController(IMediator mediator, IAccountQuery  accountQuery, IIdentityService identityService)
        {
            this._mediator = mediator;
            this._accountQuery = accountQuery;
            this._identityService = identityService;

    }
        /// <summary>
        /// 获取token 就是登录
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResultData<string>> GetToken(GetTokenCommand command)
        {
            
            return await _mediator.Send(command);
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<List<PageMenu>>> GetPageMenu()
        {
            var result= await _accountQuery.GetPageMenuByUserId(_identityService.GetUserId());
            return ResultData<List<PageMenu>>.CreateResultDataSuccess("成功", result?.children);

        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<List<UserDto>>> GetUserList(GetUserListParameter param)
        {
            var pageView = await _accountQuery.GetUserList(param);
            return ResultData<List<UserDto>>.CreateResultDataSuccess("成功", pageView.Data, pageView.Total);
        }
        /// <summary>
        ///创建用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<string>> CreateUser(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<string>> UpdateUser(UpdateUserCommand command)
        {
            return await _mediator.Send(command);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<string>> DeleteUser(DeleteUserCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<MenuTree>> GetMenuTree()
        {
            var menuTree = await _accountQuery.GetMenuTreeAsync();
            return ResultData<MenuTree>.CreateResultDataSuccess("成功",menuTree);
        }
    }

   
}