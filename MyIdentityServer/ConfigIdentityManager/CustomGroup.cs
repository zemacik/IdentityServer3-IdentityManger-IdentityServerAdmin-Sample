using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;

namespace MyIdentityServer.ConfigIdentityManager
{
  public class CustomGroup : RelationalGroup
  {
    public virtual string Description { get; set; }
  }

  public class CustomGroupService : GroupService<CustomGroup>
  {
    public CustomGroupService(CustomConfig config, CustomGroupRepository repo)
        : base(config.DefaultTenant, repo)
    {

    }
  }

  public class CustomGroupRepository : DbContextGroupRepository<CustomDatabase, CustomGroup>
  {
    public CustomGroupRepository(CustomDatabase ctx)
        : base(ctx)
    {
    }
  }

}