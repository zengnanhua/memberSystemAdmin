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
            _applicationUserRepository.AddUser(new ApplicationUser() { Name = request.Name, Sex = request.Sex, UserName = request.UserName });
            return await _applicationUserRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
