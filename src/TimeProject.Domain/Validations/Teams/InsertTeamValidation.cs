using TimeProject.Domain.Commands.Teams;

namespace TimeProject.Domain.Validations.Teams
{
    public class InsertTeamValidation : TeamValidation<InsertTeamCommand>
    {
        public InsertTeamValidation()
        {
            NameValidation();
            UserIdsValidation();
            TeamleadUserIdValidation();
        }
    }
}
