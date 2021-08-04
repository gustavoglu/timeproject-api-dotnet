using TimeProject.Domain.Commands.Customers;

namespace TimeProject.Domain.Validations.Customers
{
    public class UpdateCustomerValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerValidation()
        {
            IdValidation();
            NameValidation();
        }
    }
}
