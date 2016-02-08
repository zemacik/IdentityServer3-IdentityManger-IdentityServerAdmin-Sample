using IdentityServer3.Admin.EntityFramework;
using IdentityServer3.Admin.EntityFramework.Entities;

namespace MyIdentityServer.ConfigIdentityServerAdmin
{
    public class IdentityAdminManagerService : IdentityAdminCoreManager<IdentityClient, int, IdentityScope, int>
    {
        public IdentityAdminManagerService(string connString)
            : base(connString)
        {

        }
    }
}