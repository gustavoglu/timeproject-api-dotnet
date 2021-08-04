using TimeProject.Domain.Validations.Customers;

namespace TimeProject.Domain.Commands.Customers
{
    public class InsertCustomerCommand : CustomerCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new InsertCustomerValidation().Validate(this);
            return base.IsValid();
        }
    }
}
