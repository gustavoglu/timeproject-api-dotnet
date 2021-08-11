using TimeProject.Domain.Validations.Teams;

namespace TimeProject.Domain.Commands.Teams
{
    public class UpdateTeamCommand : TeamCommand
    {
        public override bool IsValid()
        {
            return base.IsValid(new UpdateTeamValidation().Validate(this));
        }
    }
}
