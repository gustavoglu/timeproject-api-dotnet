using FluentValidation;
using TimeProject.Domain.Commands.Projects;

namespace TimeProject.Domain.Validations.Projects
{
    public class ProjectValidation<T> : AbstractValidator<T> where T : ProjectCommand
    {
        protected void IdValidation() { RuleFor(project =>project.Id).NotNull(); }
        protected void NameValidation() { RuleFor(project => project.Name).NotNull().MaximumLength(250); }
        protected void CustomerIdValidation() { RuleFor(project => project.CustomerId).NotNull();  }
    }
}
