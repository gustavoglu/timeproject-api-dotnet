using TimeProject.Domain.Validations.Entities;

namespace TimeProject.Domain.Commands.Teams
{
    public class DeleteTeamCommand : TeamCommand
    {
        public override bool IsValid()
        {
            return base.IsValid(new EntityIdValidation<DeleteTeamCommand>().Validate(this));
        }
    }
}
