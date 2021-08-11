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
