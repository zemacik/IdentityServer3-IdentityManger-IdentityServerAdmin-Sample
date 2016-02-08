using BrockAllen.MembershipReboot.Ef;

namespace MyIdentityServer.ConfigIdentityManager
{
  public class CustomDatabase : MembershipRebootDbContext<CustomUser, CustomGroup>
  {
    public CustomDatabase(string name)
        : base(name)
    {
    }
  }
}