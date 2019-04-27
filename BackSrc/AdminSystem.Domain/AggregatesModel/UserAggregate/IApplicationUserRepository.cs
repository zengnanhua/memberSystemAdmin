using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public interface IApplicationUserRepository:IRepository<Zmn_Ac_User>
    {
        void AddUser(Zmn_Ac_User user);
    }
}
