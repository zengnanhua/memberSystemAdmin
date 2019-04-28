using AdminSystem.Api.Controllers;
using AdminSystem.Application.Commands;
using AdminSystem.Application.Queries;
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
            //Mock<IMediator> _mediatorMock = new Mock<IMediator>();

            //_mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), default(CancellationToken)));

            //HomeController homeController = new HomeController(_mediatorMock.Object);
            //var info= homeController.CreateUser().Result;

           

            //var pageView= PaginationHelp.GetPageDataAsync<UserDto>("SELECT * FROM Zmn_Ac_Users",1,20,new Dapper.DynamicParameters()
            //                    , "server=www.zengnanhua.club;port=3306;user=nanhua;password=sa123; database=AdminSystem;Allow User Variables=true;").Result;

            Console.WriteLine("Hello World!");
        }
    }
}
