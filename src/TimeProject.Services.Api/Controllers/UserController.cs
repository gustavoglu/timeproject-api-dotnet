using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Interfaces;
using TimeProject.Infra.Identity.Models;

namespace TimeProject.Services.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserController(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, IUserService userService, UserManager<User> userManager, IConfiguration configuration, IUserAuthHelper userAuthHelper) : base(bus, notifications, userAuthHelper)
        {
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("rules")]
        public async Task<IActionResult> GetRules(string userId = null)
        {
            return ResponseDefault(await _userService.GetRules(userId));
        }


        [Authorize]
        [HttpPut("rules")]
        public async Task<IActionResult> UpdateRules(string userId, [FromBody] List<KeyValuePair<string, bool>> rules)
        {
            await _userService.UpdateRules(userId, rules);
            return ResponseDefault();
        }


        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return ResponseDefault(_userService.GetById(id));
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false)
        {
            return ResponseDefault(_userService.GetAll(page, limit));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await Bus.SendCommand(command);
            return ResponseDefault();
        }

        [HttpPost("token")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserCommand command)
        {
            await Bus.SendCommand(command);

            if (Notifications.HasNotifications())
                return ResponseDefault();

            var user = _userService.GetUserByEmailAndTenanty(command.Tenanty, command.Email);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim("userid", user.Id),
                new Claim("tenanty", user.Tenanty),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var now = DateTime.Now;
            var expireted = now.AddDays(1);

            var handler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Expires = expireted,
                NotBefore = now,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(descriptor);
            var tokenWrite = handler.WriteToken(token);

            var responseToken = new
            {
                Email = user.Email,
                Name = user.Name,
                Tenanty = user.Tenanty,
                Expires = expireted,
                TokenAccess = tokenWrite,
                Claims = claims

            };

            return ResponseDefault(responseToken);
        }
    }
}
