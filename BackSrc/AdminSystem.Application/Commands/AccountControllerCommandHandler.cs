using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.Exceptions;
using AdminSystem.Infrastructure.Common;
using IdentityModel.Client;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminSystem.Application.Commands
{
    /// <summary>
    /// 获取token
    /// </summary>
    public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, ResultData<string>>
    {
        private IConfiguration _configuration;
        public GetTokenCommandHandler(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<ResultData<string>> Handle(GetTokenCommand request, CancellationToken cancellationToken)
        {

            var client = new HttpClient();

            var discoclient = new DiscoveryClient(_configuration["AuthorizationUrl"]) { Policy = { RequireHttps = false } };

            var disco = await discoclient.GetAsync();


            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("identityName", request.IdentityName);
            dic.Add("password", request.Password);
            dic.Add("Scope", "admin_api");


            var tokenResponse = await (new HttpClient()).RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "adminSystem",
                ClientSecret = "secret",
                GrantType = "user_pwd",
                Parameters = dic
            });
            if (tokenResponse.IsError)
            {
                if (tokenResponse.Json != null && tokenResponse.Json.Value<string>("resultMessage") != null)
                {
                    return ResultData<string>.CreateResultDataFail(tokenResponse.Json.Value<string>("resultMessage"));
                }
                return ResultData<string>.CreateResultDataFail(tokenResponse.Error);
            }
            else
            {
                return ResultData<string>.CreateResultDataSuccess("成功",tokenResponse.Json.Value<string>("access_token"));
            }
        }
    }
    /// <summary>
    /// 添加用户
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultData<string>>
    {
        IApplicationUserRepository _applicationUserRepository;
        public CreateUserCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            this._applicationUserRepository = applicationUserRepository;
        }
        public async Task<ResultData<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (_applicationUserRepository.GetUserByUserName(request.UserName) != null)
            {
                throw new AdminSystemDomainException("该用户名已经存在");
            }
            if (_applicationUserRepository.GetUserByPhone(request.Phone)!=null)
            {
                throw new AdminSystemDomainException("该手机号码已经绑定");
            }

            //默认密码123456
            _applicationUserRepository.AddUser(new Zmn_Ac_User(request.UserName,request.Name, "123456", request.Phone,request.Sex));
            await _applicationUserRepository.UnitOfWork.SaveEntitiesAsync();
            return ResultData<string>.CreateResultDataSuccess("成功");
        }
    }
    /// <summary>
    /// 更新用户
    /// </summary>
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultData<string>>
    {
        IApplicationUserRepository _applicationUserRepository;
        public UpdateUserCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            this._applicationUserRepository = applicationUserRepository;
        }

        public async Task<ResultData<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var temp = _applicationUserRepository.GetUserByPhone(request.Phone);
            if (temp != null && temp.Id != request.Id)
            {
                throw new AdminSystemDomainException("该手机号码已经绑定");
            }

            var user= await this._applicationUserRepository.GetUserByIdAsync(request.Id);
            //默认密码123456
            user.SetZmn_Ac_UserInfo(request.Name, request.Phone, request.Sex);
            await _applicationUserRepository.UnitOfWork.SaveEntitiesAsync();
            return ResultData<string>.CreateResultDataSuccess("成功");
        }
    }
    /// <summary>
    /// 删除用户
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultData<string>>
    {
        IApplicationUserRepository _applicationUserRepository;
        public DeleteUserCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            this._applicationUserRepository = applicationUserRepository;
        }

        public async Task<ResultData<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user= await _applicationUserRepository.GetUserByIdAsync(request.Id);
            _applicationUserRepository.Delete(user);
            await _applicationUserRepository.UnitOfWork.SaveEntitiesAsync();
            return ResultData<string>.CreateResultDataSuccess("成功");
        }
    }




}
