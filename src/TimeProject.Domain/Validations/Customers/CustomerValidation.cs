using FluentValidation;
using TimeProject.Domain.Commands.Customers;

namespace TimeProject.Domain.Validations.Customers
{
    public class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
    {
        protected void IdValidation() => RuleFor(entity => entity.Id).NotNull().NotEmpty();
        protected void NameValidation() => RuleFor(entity => entity.Name).NotNull().NotEmpty();

    }
}
