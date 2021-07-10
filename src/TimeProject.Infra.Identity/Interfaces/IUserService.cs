using System.Threading.Tasks;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Models;

namespace TimeProject.Infra.Identity.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserCommand command);

        Task<bool> SignInAsync(SignInUserCommand command);

        User GetUserByEmailAndTenanty(string tenanty, string email);
    }
}
