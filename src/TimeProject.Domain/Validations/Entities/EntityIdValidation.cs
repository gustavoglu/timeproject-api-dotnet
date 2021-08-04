using FluentValidation;
using TimeProject.Domain.Commands.Entities;

namespace TimeProject.Domain.Validations.Entities
{
    public class EntityIdValidation<T> : AbstractValidator<T> where T : EntityCommand
    {
        public EntityIdValidation()
        {
            RuleFor(entity => entity.Id).NotNull().NotEmpty();
        }
    }
}
