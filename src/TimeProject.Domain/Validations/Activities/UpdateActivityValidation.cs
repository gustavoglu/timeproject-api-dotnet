using TimeProject.Domain.Commands.Activities;

namespace TimeProject.Domain.Validations.Activities
{
    public class UpdateActivityValidation : ActivityValidation<UpdateActivityCommand>
    {
        public UpdateActivityValidation()
        {
            IdValidation();
            NameValidation();
        }
    }
}
