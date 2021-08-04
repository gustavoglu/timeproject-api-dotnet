using TimeProject.Domain.Validations.Customers;

namespace TimeProject.Domain.Commands.Customers
{
    public class UpdateCustomerCommand : CustomerCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerValidation().Validate(this);
            return base.IsValid();
        }
    }
}