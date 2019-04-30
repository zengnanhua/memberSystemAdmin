using AdminSystem.Domain.AggregatesModel.MenuAggregate;
using AdminSystem.Domain.AggregatesModel.RoleAggregate;
using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Domain.SeedWork;
using AdminSystem.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AdminSystem.Infrastructure
{
    public class ApplicationDbContext: DbContext, IUnitOfWork
    {
        public DbSet<Zmn_Ac_User> Zmn_Ac_Users { get; set; }
        public DbSet<Zmn_Ac_Role> Zmn_Ac_Roles { get; set; }
        public DbSet<Zmn_Ac_UserRole> Zmn_Ac_UserRoles { get; set; }
        public DbSet<Zmn_Ac_Menu> Zmn_Ac_Menus { get; set; }
        public DbSet<Zmn_Ac_Permission> Zmn_Ac_Permissions { get; set; }

        private readonly IMediator _mediator;

        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        private ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator)
        : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            ModelCreateConfiguration.InitApplyConfiguration(builder);
           
            base.OnModelCreating(builder);

        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await this.SaveChangesAsync();
            return true;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        if (entry.Entity.GetType().GetProperties().Where(c => c.Name == "IsDelete").Count() > 0)
                        {
                            entry.CurrentValues["IsDelete"] = true;
                        }
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    
        /// <summary>
        /// 开始事物
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }
        /// <summary>
        /// 提交事物
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        /// <summary>
        /// 回滚事物
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

    /// <summary>
    /// 生成 数据库的
    /// </summary>
    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql("server=www.zengnanhua.club;port=3306;user=nanhua;password=sa123; database=AdminSystem;", mySqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("AdminSystem.Api");
                });

            return new ApplicationDbContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken)) 
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }
        }
    }
}
