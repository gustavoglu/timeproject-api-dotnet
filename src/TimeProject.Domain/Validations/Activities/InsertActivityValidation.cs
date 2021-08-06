using TimeProject.Domain.Commands.Activities;

namespace TimeProject.Domain.Validations.Activities
{
    public class InsertActivityValidation : ActivityValidation<InsertActivityCommand>
    {
        public InsertActivityValidation()
        {
            NameValidation();
        }
    }
}
