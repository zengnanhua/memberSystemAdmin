using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

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
        public bool AddUser(ApplicationUser user)
        {
            if (user.IsTransient())
            {
                _context.ApplicationUsers
                   .Add(user);  
            }
            return true;

        }
    }
}
