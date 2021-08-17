using AutoMapper;
using TimeProject.Domain.Commands.Activities;
using TimeProject.Domain.Commands.Customers;
using TimeProject.Domain.Commands.Projects;
using TimeProject.Domain.Commands.Teams;
using TimeProject.Domain.Commands.TimeSheets;
using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Mappers
{
    public class CommandToEntity : Profile
    {
        public CommandToEntity()
        {
            CreateMap<InsertCustomerCommand, Customer>()
                .ConstructUsing(c => new Customer(c.Name, c.Code, c.Description, c.CompanyName, c.Location, c.Contact, c.Budget, c.TimeBudget));
            CreateMap<InsertCustomerCommand, Customer>()
                                .ConstructUsing(c => new Customer(c.Name, c.Code, c.Description, c.CompanyName, c.Location, c.Contact, c.Budget, c.TimeBudget) { Id = c.Id })
                                .ReverseMap();

            CreateMap<UpdateCustomerCommand, Customer>()
                 .ConstructUsing(c => new Customer(c.Name, c.Code, c.Description, c.CompanyName, c.Location, c.Contact, c.Budget, c.TimeBudget));
            CreateMap<UpdateCustomerCommand, Customer>()
                             .ConstructUsing(c => new Customer(c.Name, c.Code, c.Description, c.CompanyName, c.Location, c.Contact, c.Budget, c.TimeBudget) { Id = c.Id })
                             .ReverseMap();

            CreateMap<InsertProjectCommand, Project>();
            CreateMap<InsertProjectCommand, Project>().ReverseMap();
            CreateMap<UpdateProjectCommand, Project>();
            CreateMap<UpdateProjectCommand, Project>().ReverseMap();

            CreateMap<InsertActivityCommand, Activity>();
            CreateMap<InsertActivityCommand, Activity>().ReverseMap();
            CreateMap<UpdateActivityCommand, Activity>();
            CreateMap<UpdateActivityCommand, Activity>().ReverseMap();

            CreateMap<InsertTimeSheetCommand, TimeSheet>();
            CreateMap<InsertTimeSheetCommand, TimeSheet>().ReverseMap();
            CreateMap<UpdateTimeSheetCommand, TimeSheet>();
            CreateMap<UpdateTimeSheetCommand, TimeSheet>().ReverseMap();

            CreateMap<InsertTeamCommand, Team>();
            CreateMap<InsertTeamCommand, Team>().ReverseMap();
            CreateMap<UpdateTeamCommand, Team>();
            CreateMap<UpdateTeamCommand, Team>().ReverseMap();
        }
    }
}
