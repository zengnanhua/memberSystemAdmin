using AdminSystem.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminSystem.Application.DomainEventHandlers
{
    public class CreateUserChangeRoleDomainEventHandler : INotificationHandler<CreateUserChangeRoleDomainEvent>
    {
        public async Task Handle(CreateUserChangeRoleDomainEvent notification, CancellationToken cancellationToken)
        {
            int i = 0;
        }
    }
}
