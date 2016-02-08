using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using MyIdentityServer.ConfigIdentityManager;
using System.Collections.Generic;
using System.Linq;

namespace MyIdentityServer.ConfigIdentityServer
{
  class Factory
  {
    public static IdentityServerServiceFactory Configure(string connString)
    {
      var efConfig = new EntityFrameworkServiceOptions
      {
        ConnectionString = connString,
      };

      // these two calls just pre-populate the test DB from the in-memory config
      ConfigureClients(DefaultClients.Get(), efConfig);
      ConfigureScopes(DefaultScopes.Get(), efConfig);

      // ------------------------------------------------
      var factory = new IdentityServerServiceFactory();

      factory.RegisterConfigurationServices(efConfig);
      factory.RegisterOperationalServices(efConfig);

      // ----- MembershipReboot user service (pripajanie na ulozisko identit)
      factory.UserService = new Registration<IUserService, CustomUserService>();
      factory.Register(new Registration<CustomUserAccountService>());
      factory.Register(new Registration<CustomConfig>(CustomConfig.Config));
      factory.Register(new Registration<CustomUserRepository>());
      factory.Register(new Registration<CustomDatabase>(resolver => new CustomDatabase(connString)));

      return factory;
    }

    public static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
    {
      using (var db = new ClientConfigurationDbContext(options.ConnectionString, options.Schema))
      {
        if (!db.Clients.Any())
        {
          foreach (var c in clients)
          {
            var e = c.ToEntity();
            db.Clients.Add(e);
          }
          db.SaveChanges();
        }
      }
    }

    public static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
    {
      using (var db = new ScopeConfigurationDbContext(options.ConnectionString, options.Schema))
      {
        if (!db.Scopes.Any())
        {
          foreach (var s in scopes)
          {
            var e = s.ToEntity();
            db.Scopes.Add(e);
          }
          db.SaveChanges();
        }
      }
    }
  }
}