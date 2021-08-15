using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TimeActivity.Domain.CommandHandlers;
using TimeProject.Application.Interfaces;
using TimeProject.Application.Services;
using TimeProject.Domain.CommandHandlers;
using TimeProject.Domain.Commands.Activities;
using TimeProject.Domain.Commands.Customers;
using TimeProject.Domain.Commands.Projects;
using TimeProject.Domain.Commands.Teams;
using TimeProject.Domain.Commands.TimeSheets;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Domain.Mappers;
using TimeProject.Infra.Bus;
using TimeProject.Infra.Data.Context;
using TimeProject.Infra.Data.Repositories;
using TimeProject.Infra.Identity.CommandHandlers;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Helpers;
using TimeProject.Infra.Identity.Interfaces;
using TimeProject.Infra.Identity.Services;
using TimeTimeSheet.Domain.CommandHandlers;

namespace TimeProject.Infra.IoC
{
    public class NativeInject
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CommandToEntity));

            services.AddMediatR(Assembly.Load("TimeProject.Infra.identity"), 
                                    Assembly.Load("TimeProject.Domain"));

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped<TenantyDbContext>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<ITimeSheetRepository, TimeSheetRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();

            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITimeSheetService, TimeSheetService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAuthHelper, UserAuthHelper>();
            services.AddScoped<IRequestHandler<RegisterUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<SignInUserCommand, bool>, UserCommandHandler>();

            services.AddScoped<IRequestHandler<InsertCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCustomerCommand, bool>, CustomerCommandHandler>();

            services.AddScoped<IRequestHandler<InsertProjectCommand, bool>, ProjectCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProjectCommand, bool>, ProjectCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProjectCommand, bool>, ProjectCommandHandler>();

            services.AddScoped<IRequestHandler<InsertActivityCommand, bool>, ActivityCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateActivityCommand, bool>, ActivityCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteActivityCommand, bool>, ActivityCommandHandler>();

            services.AddScoped<IRequestHandler<InsertTimeSheetCommand, bool>, TimeSheetCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTimeSheetCommand, bool>, TimeSheetCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteTimeSheetCommand, bool>, TimeSheetCommandHandler>();

            services.AddScoped<IRequestHandler<InsertTeamCommand, bool>, TeamCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTeamCommand, bool>, TeamCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteTeamCommand, bool>, TeamCommandHandler>();








        }
    }
}
