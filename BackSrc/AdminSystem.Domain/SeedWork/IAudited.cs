using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.SeedWork
{
    public interface IAudited
    {
        int UpdateUserId { get; }
        DateTime UpdateDateTime { get; }
  
       
    }
}
