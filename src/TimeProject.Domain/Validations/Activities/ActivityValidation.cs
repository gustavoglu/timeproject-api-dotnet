using FluentValidation;
using TimeProject.Domain.Commands.Activities;

namespace TimeProject.Domain.Validations.Activities
{
    public class ActivityValidation<T> : AbstractValidator<T> where T : ActivityCommand
    {
        protected void IdValidation() { RuleFor(activity => activity.Id).NotNull(); }
        protected void NameValidation() { RuleFor(activity => activity.Name).NotNull().MaximumLength(200); }
    }
}
