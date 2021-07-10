using FluentValidation;
using TimeProject.Infra.Identity.Commands;

namespace TimeProject.Infra.Identity.Validations
{
    class SignInUserValidation : AbstractValidator<SignInUserCommand>
    {
        public SignInUserValidation()
        {
            TenantyValidation();
            EmailValidation();
            PasswordValidation();
        }
        protected void TenantyValidation() { RuleFor(command => command.Tenanty).NotNull().NotEmpty(); }
        protected void EmailValidation() { RuleFor(command => command.Email).NotNull().NotEmpty().EmailAddress(); }
        protected void PasswordValidation() { RuleFor(command => command.Password).NotNull().NotEmpty().MinimumLength(6); }

    }
}
