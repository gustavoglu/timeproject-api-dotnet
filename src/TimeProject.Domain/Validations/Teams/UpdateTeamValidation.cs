using TimeProject.Domain.Commands.Teams;

namespace TimeProject.Domain.Validations.Teams
{
    public class UpdateTeamValidation : TeamValidation<UpdateTeamCommand>
    {
        public UpdateTeamValidation()
        {
            IdValidation();
            NameValidation();
            UserIdsValidation();
            TeamleadUserIdValidation();
        }
    }
}
