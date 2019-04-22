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
    public static class dd
    {
        public static string GetQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }
        public static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        }
    }
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
}
