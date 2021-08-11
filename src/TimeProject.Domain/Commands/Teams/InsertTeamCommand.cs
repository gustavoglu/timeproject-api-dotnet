using TimeProject.Domain.Validations.Teams;

namespace TimeProject.Domain.Commands.Teams
{
    public class InsertTeamCommand : TeamCommand
    {
        public override bool IsValid()
        {
            return base.IsValid(new InsertTeamValidation().Validate(this));
        }
    }
}
