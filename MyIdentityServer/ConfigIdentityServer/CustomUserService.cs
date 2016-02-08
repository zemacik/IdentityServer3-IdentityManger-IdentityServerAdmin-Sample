using IdentityServer3.MembershipReboot;
using MyIdentityServer.ConfigIdentityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentityServer.ConfigIdentityServer
{
  public class CustomUserService : MembershipRebootUserService<CustomUser>
  {
    public CustomUserService(CustomUserAccountService userSvc)
        : base(userSvc)
    {
    }
  }
}
