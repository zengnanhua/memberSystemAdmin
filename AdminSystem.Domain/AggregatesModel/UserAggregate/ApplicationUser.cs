using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class ApplicationUser: Entity, IAggregateRoot
    {
        public ApplicationUser()
        {

        }

        public string Name { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
    }
}
