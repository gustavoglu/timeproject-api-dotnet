using TimeProject.Domain.Core.Commands;
using TimeProject.Infra.Identity.Validations;

namespace TimeProject.Infra.Identity.Commands
{
    public class SignInUserCommand : Command
    {
        public SignInUserCommand(string tenanty, string email, string password)
        {
            Tenanty = tenanty;
            Email = email;
            Password = password;
        }

        public string Tenanty { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }



        public override bool IsValid()
        {
            ValidationResult = new SignInUserValidation().Validate(this);
            return base.IsValid();
        }

    }
}
