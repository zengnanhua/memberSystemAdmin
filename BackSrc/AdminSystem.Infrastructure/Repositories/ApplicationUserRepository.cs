using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminSystem.Infrastructure.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public  void AddUser(Zmn_Ac_User user)
        {
            if (user.IsTransient())
            {
                _context.Zmn_Ac_Users
                   .Add(user);  
            }
    
        }
    }
}
