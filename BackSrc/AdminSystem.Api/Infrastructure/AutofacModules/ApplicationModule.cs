using AdminSystem.Application.Queries;
using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Infrastructure.Repositories;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminSystem.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationUserQuery(QueriesConnectionString))
                .As<IApplicationUserQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserRepository>()
               .As<IApplicationUserRepository>()
               .InstancePerLifetimeScope();
        }
    }
}
