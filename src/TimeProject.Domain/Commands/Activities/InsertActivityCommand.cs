using TimeProject.Domain.Validations.Activities;

namespace TimeProject.Domain.Commands.Activities
{
    public class InsertActivityCommand : ActivityCommand
    {
        public override bool IsValid()
        {
            return IsValid(new InsertActivityValidation().Validate(this));
        }
    }
}
