﻿using AutoMapper;
using TimeProject.Domain.Commands.Customers;
using TimeProject.Domain.Commands.Projects;
using TimeProject.Domain.Core.Entities;
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
        }
    }
}
