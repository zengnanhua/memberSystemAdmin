using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Domain.SeedWork
{
    public interface IAudited
    {
        int CreateUserId { get; }
        DateTime CreateDateTime { get;}
        int UpdateUserId { get; }
        DateTime UpdateDateTime { get; }
        int DeleteUserId { get;  }
        DateTime DeleteDateTime { get; }
        bool IsDelete { get; }
       
    }
}
