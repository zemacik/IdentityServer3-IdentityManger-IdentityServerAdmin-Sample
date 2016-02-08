using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace MyIdentityServer.ConfigIdentityServer
{
  static class DefaultUsers
  {
    public static List<InMemoryUser> Get()
    {
      return new List<InMemoryUser>
      {
          new InMemoryUser
          {
            Subject = "Michal",
            Username = "alef",
            Password = "popocatepetl",
            Claims = new Claim[]
            {
                new Claim(Constants.ClaimTypes.GivenName, "Michal"),
                new Claim(Constants.ClaimTypes.FamilyName, "Krchnavy"),
                new Claim(Constants.ClaimTypes.Email, "krchnavy@emar.sk"),
            }
          }
      };
    }
  }
}
