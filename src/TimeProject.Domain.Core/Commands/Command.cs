using FluentValidation.Results;
using TimeProject.Domain.Core.Events;

namespace TimeProject.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid() { return ValidationResult.IsValid; }
    }
}
