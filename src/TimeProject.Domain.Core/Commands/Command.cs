using FluentValidation.Results;
using TimeProject.Domain.Core.Events;

namespace TimeProject.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        protected ValidationResult ValidationResult { get;  set; }

        public ValidationResult GetValidationResult() { return ValidationResult; }
        public void SetValidationResult(ValidationResult validationResult) => ValidationResult = validationResult;

        public virtual bool IsValid() { return ValidationResult.IsValid; }
    }
}
