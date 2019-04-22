using AdminSystem.Domain.AggregatesModel.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminSystem.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        IApplicationUserRepository _applicationUserRepository;
        public CreateUserCommandHandler(IApplicationUserRepository applicationUserRepository)
        {
            this._applicationUserRepository = applicationUserRepository;
        }
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new ApplicationUser(request.UserName,request.Name,"sdfa") ;
           _applicationUserRepository.AddUser(entity);
            return await _applicationUserRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
