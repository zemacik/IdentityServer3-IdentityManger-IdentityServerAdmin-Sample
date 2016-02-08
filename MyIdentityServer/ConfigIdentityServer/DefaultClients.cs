using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace MyIdentityServer.ConfigIdentityServer
{
  static class DefaultClients
  {
    public static List<Client> Get()
    {
      return new List<Client>
      {
          new Client{
              ClientId = "idmgr_and_idadmin",
              ClientName = "IdentityManager and IdentityServer.Admin",
              Enabled = true,
              Flow = Flows.Implicit,
              RequireConsent = false,
              RedirectUris = new List<string>{
                "https://localhost:44333",
                "https://localhost:44333/idm",
                "https://localhost:44333/adm",
              },
              PostLogoutRedirectUris = new List<string>{
                "https://localhost:44333",
                "https://localhost:44333/idm",
                "https://localhost:44333/adm"
              },
              IdentityProviderRestrictions = new List<string>()
              {
                IdentityServer3.Core.Constants.PrimaryAuthenticationType
              },
              AllowedScopes = {
                IdentityServer3.Core.Constants.StandardScopes.OpenId,
                IdentityManager.Constants.IdMgrScope,
                IdentityAdmin.Constants.IdMgrScope
              }
          }
      };
    }
  }
}
