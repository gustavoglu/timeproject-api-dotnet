using TimeProject.Domain.Validations.Projects;

namespace TimeProject.Domain.Commands.Projects
{
    public class InsertProjectCommand : ProjectCommand
    {
        public override bool IsValid()
        {
            SetValidationResult(new InsertProjectValidation().Validate(this));
            return base.IsValid();
        }
    }
}
