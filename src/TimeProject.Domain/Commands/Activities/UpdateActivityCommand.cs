using TimeProject.Domain.Validations.Activities;

namespace TimeProject.Domain.Commands.Activities
{
    public class UpdateActivityCommand : ActivityCommand
    {
        public override bool IsValid()
        {
            return IsValid(new UpdateActivityValidation().Validate(this));
        }
    }
}
