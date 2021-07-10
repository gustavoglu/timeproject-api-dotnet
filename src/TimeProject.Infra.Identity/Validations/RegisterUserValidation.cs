using FluentValidation;
using TimeProject.Infra.Identity.Commands;

namespace TimeProject.Infra.Identity.Validations
{
    public class RegisterUserValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidation()
        {
            TenantyValidation();
            NameValidation();
            EmailValidation();
            PasswordValidation();
            ConfirmPasswordValidation();
        }
        protected void TenantyValidation() { RuleFor(command => command.Tenanty).NotNull().NotEmpty(); }
        protected void NameValidation() { RuleFor(command => command.Name).NotNull().NotEmpty().MaximumLength(30); }
        protected void EmailValidation() { RuleFor(command => command.Email).NotNull().NotEmpty().EmailAddress(); }
        protected void PasswordValidation() { RuleFor(command => command.Password).NotNull().NotEmpty().MinimumLength(6); }
        protected void ConfirmPasswordValidation() { RuleFor(command => command.ConfirmPassword).NotNull().NotEmpty().Equal(command => command.Password); }
  
    }
}
