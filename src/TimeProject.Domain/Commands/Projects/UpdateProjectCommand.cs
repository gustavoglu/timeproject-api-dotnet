using TimeProject.Domain.Validations.Projects;

namespace TimeProject.Domain.Commands.Projects
{
    public class UpdateProjectCommand : ProjectCommand
    {
        public override bool IsValid()
        {
            SetValidationResult(new UpdateProjectValidation().Validate(this));
            return base.IsValid();
        }
    }
}
