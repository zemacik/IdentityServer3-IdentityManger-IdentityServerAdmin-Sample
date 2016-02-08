using IdentityManager;
using IdentityManager.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace MyIdentityServer.ConfigIdentityManager
{
  class Factory
  {
    public static IdentityManagerServiceFactory Configure(string connString)
    {
      var factory = new IdentityManagerServiceFactory();

      ConfigureUsers(DefaultUsers.Get(), connString);

      factory.IdentityManagerService = new Registration<IIdentityManagerService, CustomIdentityManagerService>();
      factory.Register(new Registration<CustomUserAccountService>());
      factory.Register(new Registration<CustomGroupService>());
      factory.Register(new Registration<CustomUserRepository>());
      factory.Register(new Registration<CustomGroupRepository>());
      factory.Register(new Registration<CustomDatabase>(resolver => new CustomDatabase(connString)));
      factory.Register(new Registration<CustomConfig>(CustomConfig.Config));

      return factory;
    }

    public static void ConfigureUsers(IEnumerable<DefaultUser> users, string connString)
    {
      using (var db = new CustomDatabase(connString))
      {
        if (!db.Users.Any())
        {
          var repo = new CustomUserRepository(db);
          var svc = new CustomUserAccountService(CustomConfig.Config, repo);
        
          foreach (var u in users)
          {
            var account = svc.CreateAccount(u.UserName, u.Password, u.Email);
            account.FirstName = u.FirstName;
            account.LastName = u.LastName;
            account.Age = u.Age;
            svc.Update(account);            

            foreach (var role in u.Roles)
              svc.AddClaim(account.ID, IdentityManager.Constants.ClaimTypes.Role, role);
          }
          db.SaveChanges();
        }
      }
    }

  }
}