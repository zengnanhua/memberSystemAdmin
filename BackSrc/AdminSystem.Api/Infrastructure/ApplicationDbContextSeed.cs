using AdminSystem.Domain.AggregatesModel.UserAggregate;
using AdminSystem.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminSystem.Api.Infrastructure
{
    public class ApplicationDbContextSeed
    {
        public async Task SeedAsync(ApplicationDbContext context, IHostingEnvironment env)
        {
            if (!context.ApplicationUsers.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    
                    Name="小华",
                    Sex="男",
                    UserName="15889421601"
                };
                context.ApplicationUsers.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
