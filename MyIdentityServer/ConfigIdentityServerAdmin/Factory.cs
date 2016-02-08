using IdentityAdmin.Configuration;
using IdentityAdmin.Core;

namespace MyIdentityServer.ConfigIdentityServerAdmin
{
  public class Factory
  {
    public static IdentityAdminServiceFactory Configure(string connString)
    {
      var factory = new IdentityAdminServiceFactory();
      factory.IdentityAdminService = new Registration<IIdentityAdminService>(resolver => new IdentityAdminManagerService(connString));
      return factory;
    }
  }
}