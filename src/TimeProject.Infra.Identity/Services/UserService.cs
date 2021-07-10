using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Interfaces;
using TimeProject.Infra.Identity.Models;

namespace TimeProject.Infra.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMediatorHandler _bus;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMediatorHandler bus)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bus = bus;
        }

        private void SendNotificationsByIdentityResult(IdentityResult identityResult)
        {
            if (identityResult.Succeeded) return;

            foreach (var error in identityResult.Errors)
                _bus.RaiseEvent(new DomainNotification("Identity", error.Description));
        }

        public async Task RegisterAsync(RegisterUserCommand command)
        {
            var userExists = _userManager.Users.FirstOrDefault(u => u.Email.ToLower() == command.Email.ToLower() &&
                                                                    u.Tenanty.ToLower() == command.Tenanty.ToLower());
            if (userExists != null)
            {
                await _bus.RaiseEvent(new DomainNotification("Identity", "User already exists"));
                return;
            }

            var user = new User(command.Email, command.Tenanty, command.Name);
            var res = await _userManager.CreateAsync(user, command.Password);
            SendNotificationsByIdentityResult(res);
            return;
        }


        public async Task<bool> SignInAsync(SignInUserCommand command)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email.ToLower() == command.Email.ToLower() &&
                                                                 u.Tenanty.ToLower() == command.Tenanty.ToLower());

            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("Identity", "Email or Password Incorrect"));
                return false;
            }

            var res = await _signInManager.PasswordSignInAsync(user, command.Password, false, false);

            if (!res.Succeeded)
            {
                await _bus.RaiseEvent(new DomainNotification("Identity", "Email or Password Incorrect"));
                return false;
            }

            return true;
        }


        public User GetUserByEmailAndTenanty(string tenanty, string email)
        {
            return _userManager.Users.FirstOrDefault(u => u.Email == email &&
                                                                u.Tenanty == tenanty);
        }
    }
}
