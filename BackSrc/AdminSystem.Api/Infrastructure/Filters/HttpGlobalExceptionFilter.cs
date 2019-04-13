using AdminSystem.Domain.Exceptions;
using AdminSystem.Infrastructure.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminSystem.Api.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {

            //if (env.IsDevelopment())
            if (context.Exception.GetType() == typeof(AdminSystemDomainException))
            {
                string errorMessage = " ";
                if (context.Exception.InnerException != null
                    && context.Exception.InnerException.GetType() == typeof(FluentValidation.ValidationException))
                {
                    var validationE= context.Exception.InnerException as FluentValidation.ValidationException;
                    foreach (var temp in validationE.Errors)
                    {
                        errorMessage += temp.ErrorMessage+",";
                    }
                }

                context.Result = new ObjectResult(ResultData<string>.CreateResultDataFail(
                    context.Exception.Message+","+ errorMessage.Substring(0, errorMessage.Length-1)));
            }
            else
            {
                context.Result = new ObjectResult(ResultData<string>.CreateResultDataFail(context.Exception.Message));
            }
            
            
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            context.ExceptionHandled = true;
        }
    }
}
