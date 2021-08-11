using FluentValidation;
using TimeProject.Domain.Commands.Teams;

namespace TimeProject.Domain.Validations.Teams
{
    public class TeamValidation<T> : AbstractValidator<T> where T : TeamCommand
    {
        protected void IdValidation() => RuleFor(team => team.Id).NotNull();

        protected void NameValidation() => RuleFor(team => team.Name).NotNull().MaximumLength(200);

        protected void UserIdsValidation() =>
            RuleFor(team => team.UserIds).NotNull().NotEmpty();

        protected void TeamleadUserIdValidation() =>
        RuleFor(team => team.UserIds).NotNull();
    }
}
