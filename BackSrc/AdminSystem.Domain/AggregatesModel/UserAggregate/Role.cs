using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.AggregatesModel.UserAggregate
{
    public class Role: Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        
    }
}
