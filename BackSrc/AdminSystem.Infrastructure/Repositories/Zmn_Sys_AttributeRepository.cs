using AdminSystem.Domain.AggregatesModel.AttributeAggregate;
using AdminSystem.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminSystem.Infrastructure.Repositories
{
    public class Zmn_Sys_AttributeRepository : IZmn_Sys_AttributeRepository
    {
        private readonly ApplicationDbContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public Zmn_Sys_AttributeRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddAttribute(Zmn_Sys_Attribute attribute)
        {
            _context.Zmn_Sys_Attributes
                  .Add(attribute);
        }
    }
}
