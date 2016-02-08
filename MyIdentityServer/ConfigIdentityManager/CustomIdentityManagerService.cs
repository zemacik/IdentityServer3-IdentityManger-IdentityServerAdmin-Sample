using IdentityManager.MembershipReboot;

namespace MyIdentityServer.ConfigIdentityManager
{
  public class CustomIdentityManagerService : MembershipRebootIdentityManagerService<CustomUser, CustomGroup>
  {
    public CustomIdentityManagerService(CustomUserAccountService userSvc, CustomGroupService groupSvc)
        : base(userSvc, groupSvc)
    {
    }
  }
}