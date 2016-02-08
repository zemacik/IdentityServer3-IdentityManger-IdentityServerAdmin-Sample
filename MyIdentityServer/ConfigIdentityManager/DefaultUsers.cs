using System.Collections.Generic;

namespace MyIdentityServer.ConfigIdentityManager
{
  static class DefaultUsers
  {
    public static List<DefaultUser> Get()
    {
      return new List<DefaultUser>
      {
        new DefaultUser {
          UserName ="admin",
          Password ="admin",
          Email ="foo@bar.com",
          FirstName ="foo",
          LastName ="bar",
          Roles = new List<string>
          {
            "IdentityServerAdmin",
            "IdentityManagerAdmin"
          }
        }
      };
    }
  }

  public class DefaultUser
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? Age { get; set; }
    public List<string> Roles { get; set; }

    public DefaultUser()
    {
      Roles = new List<string>();
    }
  }
}
