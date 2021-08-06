using TimeProject.Domain.Validations.Entities;

namespace TimeProject.Domain.Commands.Projects
{
    public class DeleteProjectCommand : ProjectCommand
    {
        public override bool IsValid()
        {
            SetValidationResult(new EntityIdValidation<DeleteProjectCommand>().Validate(this));
            return base.IsValid();
        }
    }
}
