using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.AuthenticationValidator;

namespace UserIdentity
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId="adminSystem",
                    ClientName="adminSystemApi",
                    ClientSecrets=new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    RefreshTokenExpiration=TokenExpiration.Sliding,
                    AllowOfflineAccess=true,
                    AllowedGrantTypes=new List<string>{ ValidatorConst.UserPwd },
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowedScopes=new List<string>
                    {
                        "admin_api",
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }

                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("admin_api","admin_api")
            };
        }
    }
}
