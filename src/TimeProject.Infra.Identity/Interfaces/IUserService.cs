using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Models;

namespace TimeProject.Infra.Identity.Interfaces
{
    public interface IUserService
    {
        Task AddClaim(string userId, string type, string value);
        Task AddClaims(string userId, IEnumerable<Claim> claims);
        Task<IEnumerable<Claim>> GetClaims(string userId);
        Task RegisterAsync(RegisterUserCommand command);
        Task<bool> SignInAsync(SignInUserCommand command);
        User GetUserByEmailAndTenanty(string tenanty, string email);
        Task RemoveClaims(string userId, IEnumerable<Claim> claims);
    }
}
