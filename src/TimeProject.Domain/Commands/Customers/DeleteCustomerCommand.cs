using TimeProject.Domain.Validations.Entities;

namespace TimeProject.Domain.Commands.Customers
{
    public class DeleteCustomerCommand : CustomerCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new EntityIdValidation<DeleteCustomerCommand>().Validate(this);
            return base.IsValid();
        }
    }
}
