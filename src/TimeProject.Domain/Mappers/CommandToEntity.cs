using AutoMapper;
using TimeProject.Domain.Commands.Activities;
using TimeProject.Domain.Commands.Customers;
using TimeProject.Domain.Commands.Projects;
using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Mappers
{
    public class CommandToEntity : Profile
    {
        public CommandToEntity()
        {
            CreateMap<InsertCustomerCommand, Customer>();
            CreateMap<InsertCustomerCommand, Customer>().ReverseMap();
            CreateMap<UpdateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>().ReverseMap();

            CreateMap<InsertProjectCommand, Project>();
            CreateMap<InsertProjectCommand, Project>().ReverseMap();
            CreateMap<UpdateProjectCommand, Project>();
            CreateMap<UpdateProjectCommand, Project>().ReverseMap();

            CreateMap<InsertActivityCommand, Activity>();
            CreateMap<InsertActivityCommand, Activity>().ReverseMap();
            CreateMap<UpdateActivityCommand, Activity>();
            CreateMap<UpdateActivityCommand, Activity>().ReverseMap();
        }
    }
}
