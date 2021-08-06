using FluentValidation;
using TimeProject.Domain.Commands.TimeSheets;

namespace TimeProject.Domain.Validations.TimeSheets
{
    public class TimeSheetValidation<T> : AbstractValidator<T> where T : TimeSheetCommand
    {
        protected void IdValidation() { RuleFor(timeSheet => timeSheet.Id).NotNull(); }
        protected void DescriptionValidation() { RuleFor(timeSheet => timeSheet.Description).MaximumLength(500); }
        protected void StartDateValidation() { RuleFor(timeSheet => timeSheet.StartDate).NotNull().NotEmpty(); }
        protected void ProjectIdValidation() { RuleFor(timeSheet => timeSheet.ProjectId).NotNull(); }
        protected void ActivityIdValidation() { RuleFor(timeSheet => timeSheet.ActivityId).NotNull(); }
        protected void UserIdValidation() { RuleFor(timeSheet => timeSheet.UserId).NotNull(); }
    }
}
