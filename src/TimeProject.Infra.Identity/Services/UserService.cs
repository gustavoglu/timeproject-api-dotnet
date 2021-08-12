using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

;            var res = await _userManager.CreateAsync(user, command.Password);
            if (!res.Succeeded)
            {
                SendNotificationsByIdentityResult(res);
                return;
            }

            await AddClaim(user.Id, "rule", "master");
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

        public async Task RemoveClaims(string userId, IEnumerable<Claim> claims)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("UserService", "User not found"));
                return;
            }

            var claimsUser = await _userManager.GetClaimsAsync(user);

            var claimsExists = from claimUser in claimsUser
                               where claims.ToList()
                                     .Exists(c => c.Type == claimUser.Type && c.Value == claimUser.Value)
                               select claimUser;

            if (claimsExists.Any())
                await _userManager.RemoveClaimsAsync(user, claimsExists);
        }

        public async Task RemoveClaim(string userId, string type, string value)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("UserService", "User not found"));
                return;
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var claimExists = claims.Where(claim => claim.Type == type && claim.Value == value);
            await _userManager.RemoveClaimsAsync(user, claimExists);
        }

        public async Task AddClaim(string userId, string type, string value)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("UserService", "User not found"));
                return;
            }
            var claims = await _userManager.GetClaimsAsync(user);

            var claimExists = claims.Where(claim => claim.Type == type && claim.Value == value);

            if (claimExists != null)
                await _userManager.RemoveClaimsAsync(user, claimExists);

            await _userManager.AddClaimAsync(user, new Claim(type, value));
        }

        public async Task AddClaims(string userId, IEnumerable<Claim> claims)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("UserService", "User not found"));
                return;
            }
            var claimsUser = await _userManager.GetClaimsAsync(user);

            var claimsExists = from claimUser in claimsUser
                               where claims.ToList()
                                     .Exists(c => c.Type == claimUser.Type && c.Value == claimUser.Value)
                               select claimUser;

            if (claimsExists.Any())
                await _userManager.RemoveClaimsAsync(user, claimsExists);

            await _userManager.AddClaimsAsync(user, claims);
        }

        public async Task<IEnumerable<Claim>> GetClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("UserService", "User not found"));
                return null;
            }

            return await _userManager.GetClaimsAsync(user);
        }
    }
}
