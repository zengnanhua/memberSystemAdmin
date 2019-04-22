using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminSystem.Application.Commands;
using AdminSystem.Infrastructure.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AdminSystem.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        public async Task<ResultData<string>> GetToken(GetTokenCommand command)
        {
        
            return await _mediator.Send(command);
        }
            
    }

   
}