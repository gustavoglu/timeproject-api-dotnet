using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Pagination;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Interfaces;
using TimeProject.Infra.Identity.Models;
using TimeProject.Infra.Identity.Rules;
using TimeProject.Infra.Identity.ViewModels;

namespace TimeProject.Infra.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMediatorHandler _bus;
        private readonly IUserAuthHelper _userAuthHelper;
        private readonly IMapper _mapper;


        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMediatorHandler bus, IUserAuthHelper userAuthHelper, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bus = bus;
            _userAuthHelper = userAuthHelper;
            _mapper = mapper;
        }


        public async Task UpdateRules(string userId, List<KeyValuePair<string, bool>> rules)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("UserService", "User not found"));
                return;
            }

            var rulesToClaimList = rules.Where(rule => rule.Value == true).Select(rule => new Claim("rule", rule.Key));

            var claims = await GetClaims(userId);

            var claimsRule = claims.Where(claim => claim.Type == "rule").ToList();

            await _userManager.RemoveClaimsAsync(user, claimsRule);

            await _userManager.AddClaimsAsync(user, rulesToClaimList);

        }

        public async Task<List<KeyValuePair<string, bool>>> GetRules(string userId = null)
        {
            var rules = Enum.GetNames(typeof(ERule)).ToList();

            if (userId == null)
                return rules.Select(rule => new KeyValuePair<string, bool>(rule, false)).ToList();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("UserService", "User not found"));
                return null;
            }

            var claims = (await GetClaims(userId)).ToList();


            var rulesSelect = from rule in rules
                              select new KeyValuePair<string, bool>(rule, claims.Exists(claim => claim.Type == "rule" && claim.Value == rule));

            return rulesSelect.ToList();

        }

        private bool NoUsersInTenanty(string tenanty)
        {
            return !_userManager.Users.ToList().Exists(user => user.Tenanty == tenanty);
        }

        public UserSimpleViewModel GetById(string id)
        {
            string tenanty = _userAuthHelper.GetTenanty();
            var user = _userManager.Users.Where(user => user.Tenanty == tenanty).FirstOrDefault();
            return new UserSimpleViewModel() { Id = user.Id, Email = user.Email, Name = user.Name };
        }

        public PaginationData<UserSimpleViewModel> GetAll(int? page = null, int? limit = null)
        {
            string tenanty = _userAuthHelper.GetTenanty();
            long total = _userManager.Users.Where(user => user.Tenanty == tenanty).Count();

            if (page.HasValue && limit.HasValue)
            {
                return new PaginationData<UserSimpleViewModel>(
                _userManager.Users
                 .Where(user => user.Tenanty == tenanty)
                 .Skip((page.Value - 1) * limit.Value)
                 .Take(limit.Value)
                 .Select(user => new UserSimpleViewModel() { Id = user.Id, Email = user.Email, Name = user.Name })
                 .OrderBy(user => user.Email)
                 .ToList(), limit, page, total);
            }


            return new PaginationData<UserSimpleViewModel>(_userManager.Users
                                                                 .Where(user => user.Tenanty == tenanty)
                                                                 .Select(user => new UserSimpleViewModel() { Id = user.Id, Email = user.Email, Name = user.Name })
                                                                 .OrderBy(user => user.Email)
                                                                 .ToList(), limit, page, total);
        }

        public List<UserSimpleViewModel> GetUsersSimple()
        {
            var users = _userManager.Users.Where(user => user.Tenanty == _userAuthHelper.GetTenanty()).ToList();
            var usersSimple = _mapper.Map<List<UserSimpleViewModel>>(users);
            return usersSimple;
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

            bool noUsersInTenanty = NoUsersInTenanty(command.Tenanty);

            var res = await _userManager.CreateAsync(user, command.Password);
            if (!res.Succeeded)
            {
                SendNotificationsByIdentityResult(res);
                return;
            }

            if (noUsersInTenanty)
                await AddClaim(user.Id, "rule", ERule.Master.ToString());

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
