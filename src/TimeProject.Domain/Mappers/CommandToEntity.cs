using AutoMapper;
using TimeProject.Domain.Commands.Customers;
using TimeProject.Domain.Core.Commands;

namespace TimeProject.Domain.Mappers
{
    public class CommandToEntity : Profile
    {
        public CommandToEntity()
        {
            CreateMap<InsertCustomerCommand, Command>();
            CreateMap<InsertCustomerCommand, Command>().ReverseMap();

            CreateMap<UpdateCustomerCommand, Command>();
            CreateMap<UpdateCustomerCommand, Command>().ReverseMap();
        }
    }
}
