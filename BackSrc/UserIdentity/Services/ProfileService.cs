using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserIdentity.Services
{
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var list = new List<System.Security.Claims.Claim>();
            list.Add(new System.Security.Claims.Claim("userName", "admin"));
            context.IssuedClaims = context.Subject.Claims.ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return;
        }
    }
}
