using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Application.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string UserName { get; private set; }   
        public string Name { get; private set; }
        public string Sex { get; private set; }

        public CreateUserCommand(string UserName, string Name, string Sex)
        {
            this.UserName = UserName;
            this.Name = Name;
            this.Sex = Sex;
        }
    }
}
