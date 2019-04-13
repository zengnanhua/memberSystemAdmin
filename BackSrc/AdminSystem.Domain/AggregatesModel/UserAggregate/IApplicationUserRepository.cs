using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public interface IApplicationUserRepository:IRepository<ApplicationUser>
    {
        bool AddUser(ApplicationUser user);
    }
}
