using TimeProject.Domain.Core.Commands;
using TimeProject.Infra.Identity.Validations;

namespace TimeProject.Infra.Identity.Commands
{
    public class RegisterUserCommand : Command
    {
        public RegisterUserCommand(string tenanty, string name, string email, string password, string confirmPassword)
        {
            Tenanty = tenanty;
            Name = name;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string Tenanty { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public override bool IsValid()
        {
            SetValidationResult(new RegisterUserValidation().Validate(this));
            return base.IsValid();
        }
    }
}
