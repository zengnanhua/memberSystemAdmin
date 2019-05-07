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

namespace unitTest
{
   
    class Program
    {
        
        static void Main(string[] args)
        {

            var enumList= typeof(PlatformType).GetTypeInfo().Assembly.GetTypes().Where(c => c.GetCustomAttribute(typeof(EnumRemarkAttribute)) != null&&c.IsEnum).ToList();
            foreach (var item in enumList)
            {
                var headAttribute= item.GetCustomAttribute<EnumRemarkAttribute>();

                var fields = item.GetEnumNames();
                foreach (var fieldItem in fields)
                {
                    var bodyAttribute = item.GetField(fieldItem).GetCustomAttribute<EnumRemarkAttribute>();
                    if (bodyAttribute==null)
                    {
                        throw new Exception($"{headAttribute.Remark}中的值‘{fieldItem}’没有加属性标签");
                    }

                }

            }

            Console.WriteLine("Hello World!");
        }
    }
}
