using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminSystem.Application.Queries
{
    public interface IApplicationUserQuery
    {
        Task<ApplicationUserViewModel> GetUserAsync(int id);
    }
}
