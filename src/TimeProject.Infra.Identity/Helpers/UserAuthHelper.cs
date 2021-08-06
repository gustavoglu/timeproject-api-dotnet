using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TimeProject.Domain.Interfaces;

namespace TimeProject.Infra.Identity.Helpers
{
    public class UserAuthHelper : IUserAuthHelper
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAuthHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IEnumerable<Claim> GetClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public bool HasClaim(string type, string value)
        {
            return _accessor.HttpContext.User.HasClaim(claim => claim.Type == type && claim.Value == value);
        }

        public string GetId()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "userid")?.Value; ;
        }

        public string GetTenanty()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "tenanty")?.Value; ;
        }

        public string GetUserName()
        {
            return _accessor.HttpContext.User.Identity.Name;
        }
    }
}
