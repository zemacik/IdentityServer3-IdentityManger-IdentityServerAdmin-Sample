using IdentityAdmin.Configuration;
using IdentityManager.Configuration;
using IdentityManager.Extensions;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Serilog;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using WebHost.ConfigIdentityServer;

[assembly: OwinStartup(typeof(MyIdentityServer.Startup))]

namespace MyIdentityServer
{
  public class Startup
  {
    public void Configuration(IAppBuilder appBuilder)
    {
      Log.Logger = new LoggerConfiguration()
        .WriteTo.Trace(outputTemplate: "{Timestamp} [{Level}] ({Name}){NewLine} {Message}{NewLine}{Exception}")
        .CreateLogger();

      JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

      appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = "Cookies",
        LoginPath = new PathString("/Home/Login")
      });

      appBuilder.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
      {
        AuthenticationType = "oidc",
        Authority = "https://localhost:44333",
        ClientId = "idmgr_and_idadmin",
        RedirectUri = "https://localhost:44333",
        ResponseType = "id_token",
        UseTokenLifetime = false,
        Scope = "openid idmgr idAdmin",
        SignInAsAuthenticationType = "Cookies",
        Notifications = new OpenIdConnectAuthenticationNotifications
        {
          SecurityTokenValidated = n =>
          {
            n.AuthenticationTicket.Identity.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));
            return Task.FromResult(0);
          },
          RedirectToIdentityProvider = async n =>
          {
            if (n.ProtocolMessage.RequestType == Microsoft.IdentityModel.Protocols.OpenIdConnectRequestType.LogoutRequest)
            {
              var result = await n.OwinContext.Authentication.AuthenticateAsync("Cookies");
              if (result != null)
              {
                var id_token = result.Identity.Claims.GetValue("id_token");
                if (id_token != null)
                {
                  n.ProtocolMessage.IdTokenHint = id_token;
                  n.ProtocolMessage.PostLogoutRedirectUri = "https://localhost:44333";
                }
              }
            }
          }
        }
      });

      var connString = ConfigurationManager.ConnectionStrings["IdSvr3Config"].ConnectionString;

      // Identity admin
      appBuilder.Map("/adm", adminApp =>
      {
        var idAdminOptions = new IdentityAdminOptions
        {
          Factory = ConfigIdentityServerAdmin.Factory.Configure(connString),
          AdminSecurityConfiguration = new IdentityAdmin.Configuration.AdminHostSecurityConfiguration()
          {
            HostAuthenticationType = IdentityAdmin.Constants.CookieAuthenticationType,
            AdminRoleName = "IdentityServerAdmin",
            NameClaimType = "name",
            RoleClaimType = "role",
          }

        };

        adminApp.UseIdentityAdmin(idAdminOptions);
      });

      //Identity manager
      appBuilder.Map("/idm", adminApp =>
      {
        var idManagerOptions = new IdentityManagerOptions
        {
          Factory = ConfigIdentityManager.Factory.Configure(connString),
          SecurityConfiguration = new HostSecurityConfiguration()
          {
            HostAuthenticationType = IdentityManager.Constants.CookieAuthenticationType,
            AdminRoleName = "IdentityManagerAdmin",
            NameClaimType = "name",
            RoleClaimType = "role",
          }
        };

        adminApp.UseIdentityManager(idManagerOptions);
      });

      // Identity server
      appBuilder.Map("/ids", adminApp =>
      {
        var idsrvOptions = new IdentityServerOptions
        {
          Factory = ConfigIdentityServer.Factory.Configure(connString),
          SigningCertificate = Certificate.Get(),
          RequireSsl = true
        };

        appBuilder.UseIdentityServer(idsrvOptions);
      });

    }
  }
}