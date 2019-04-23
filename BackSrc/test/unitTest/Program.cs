using AdminSystem.Api.Controllers;
using AdminSystem.Application.Commands;
using MediatR;
using Moq;
using System;
using System.Threading;

namespace unitTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Mock<IMediator> _mediatorMock = new Mock<IMediator>();

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), default(CancellationToken)));

            HomeController homeController = new HomeController(_mediatorMock.Object);
            var info= homeController.CreateUser().Result;
            Console.WriteLine("Hello World!");
        }
    }
}
