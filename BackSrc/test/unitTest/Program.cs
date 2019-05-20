using AdminSystem.Api.Controllers;
using AdminSystem.Application.Commands;
using AdminSystem.Application.Queries;
using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.CommonClass;
using MediatR;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace unitTest
{
   
    public class Program
    {

        string str = "121";
        static void Main(string[] args)
        {
            Person person1 = new Person() { Code="0123"};
            Person person2 = new Person() { Code="0123"};
            Task.Run(() =>
            {
                new SS().Start("bb");
            });
            Task.Run(() =>
            {
                new SS().Start("bb");
            });
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
    public class Person
    {
        public string Code { get; set; }
    }
    public class SS
    {
        private string bb = "dfas";
        public void Start(string str)
        {
            Monitor.Enter(str);
            Console.WriteLine("ok");
            Thread.Sleep(10000);
            Monitor.Exit(str);
        }
    }
}
