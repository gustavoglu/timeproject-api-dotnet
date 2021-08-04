using System;

namespace TimeProject.Presentation.Mobile.App.Models
{
    public class UserParamsResponse
    {
        public UserParamsResponse(string email, string name, string tenanty, DateTime expires, string tokenAccess)
        {
            Email = email;
            Name = name;
            Tenanty = tenanty;
            Expires = expires;
            TokenAccess = tokenAccess;
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Tenanty { get; set; }
        public DateTime Expires { get; set; }
        public string TokenAccess { get; set; }
    }
}
