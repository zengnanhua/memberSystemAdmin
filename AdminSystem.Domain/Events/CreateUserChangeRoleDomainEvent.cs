using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.Events
{
    public class CreateUserChangeRoleDomainEvent: INotification
    {
        public string UserName { get; }
        public CreateUserChangeRoleDomainEvent(string userName)
        {
            this.UserName = userName;
        }
    }
}
