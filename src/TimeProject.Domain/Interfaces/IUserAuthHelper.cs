using System.Collections.Generic;
using System.Security.Claims;

namespace TimeProject.Domain.Interfaces
{
    public interface IUserAuthHelper
    {
        bool HasClaim(string type, string value);
        IEnumerable<Claim> GetClaims();
        string GetId();
        string GetUserName();
        string GetTenanty();
    }
}
