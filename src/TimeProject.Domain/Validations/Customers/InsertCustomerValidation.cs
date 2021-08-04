using TimeProject.Domain.Commands.Customers;

namespace TimeProject.Domain.Validations.Customers
{
    public class InsertCustomerValidation : CustomerValidation<InsertCustomerCommand>
    {
        public InsertCustomerValidation()
        {
            NameValidation();
        }
    }
}
