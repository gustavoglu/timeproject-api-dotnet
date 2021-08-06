using TimeProject.Domain.Commands.Projects;

namespace TimeProject.Domain.Validations.Projects
{
    public class InsertProjectValidation : ProjectValidation<InsertProjectCommand>
    {
        public InsertProjectValidation()
        {
            NameValidation();
            CustomerIdValidation();
        }
    }
}
