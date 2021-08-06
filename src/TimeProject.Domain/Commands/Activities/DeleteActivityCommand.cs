using TimeProject.Domain.Validations.Entities;

namespace TimeProject.Domain.Commands.Activities
{
    public class DeleteActivityCommand : ActivityCommand
    {
        public override bool IsValid()
        {
            return IsValid(new EntityIdValidation<DeleteActivityCommand>().Validate(this));
        }
    }
}
