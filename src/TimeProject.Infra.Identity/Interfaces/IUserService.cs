using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeProject.Domain.Pagination;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Models;
using TimeProject.Infra.Identity.ViewModels;

namespace TimeProject.Infra.Identity.Interfaces
{
    public interface IUserService
    {
        List<UserSimpleViewModel> GetUsersSimple();
        Task AddClaim(string userId, string type, string value);
        Task AddClaims(string userId, IEnumerable<Claim> claims);
        Task<IEnumerable<Claim>> GetClaims(string userId);
        Task RegisterAsync(RegisterUserCommand command);
        Task<bool> SignInAsync(SignInUserCommand command);
        User GetUserByEmailAndTenanty(string tenanty, string email);
        Task RemoveClaims(string userId, IEnumerable<Claim> claims);
        Task UpdateRules(string userId, List<KeyValuePair<string, bool>> rules);
        Task<List<KeyValuePair<string, bool>>> GetRules(string userId = null);
        UserSimpleViewModel GetById(string id);
        PaginationData<UserSimpleViewModel> GetAll(int? page = null, int? limit = null);
    }
}
