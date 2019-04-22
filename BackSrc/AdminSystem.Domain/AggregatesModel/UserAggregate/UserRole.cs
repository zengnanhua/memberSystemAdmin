using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class UserRole : Entity
    {
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser{ get; private set; }
        public int RoleId { get; set; }
        public Role Role { get; private set; }
    }
}
