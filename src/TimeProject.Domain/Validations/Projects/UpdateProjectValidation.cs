using TimeProject.Domain.Commands.Projects;

namespace TimeProject.Domain.Validations.Projects
{
    public class UpdateProjectValidation : ProjectValidation<UpdateProjectCommand>
    {
        public UpdateProjectValidation()
        {
            IdValidation();   
            NameValidation();
            CustomerIdValidation();
        }
    }
}
