using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace MyIdentityServer.ConfigIdentityServer
{
  static class DefaultScopes
  {
    public static List<Scope> Get()
    {
      return new List<Scope>
      {
          StandardScopes.OpenId,
          StandardScopes.Profile,
          StandardScopes.Email,
          StandardScopes.OfflineAccess,
          new Scope{
            Name = IdentityManager.Constants.IdMgrScope,
            DisplayName = "IdentityManager",
            Description = "Authorization for IdentityManager",
            Type = ScopeType.Identity,
            Claims = new List<ScopeClaim>{
              new ScopeClaim(Constants.ClaimTypes.Name),
              new ScopeClaim(Constants.ClaimTypes.Role)
            }
          },
          new Scope{
            Name = IdentityAdmin.Constants.IdMgrScope,
            DisplayName = "IdentityServer.Admin",
            Description = "Authorization for IdentityServer.Admin",
            Type = ScopeType.Identity,
            Claims = new List<ScopeClaim>{
              new ScopeClaim(Constants.ClaimTypes.Name),
              new ScopeClaim(Constants.ClaimTypes.Role)
            }
          },
      };
    }
  }
}
