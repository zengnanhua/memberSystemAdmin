using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminSystem.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminSystem.Api.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]/[action]")]
   
    public class HomeController : Controller
    {
        IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
        [HttpGet]
        public async Task<bool> CreateUser()
        {
            CreateUserCommand createUserCommand = new CreateUserCommand("115516","","女");
            return await _mediator.Send(createUserCommand);
        }

    }
}