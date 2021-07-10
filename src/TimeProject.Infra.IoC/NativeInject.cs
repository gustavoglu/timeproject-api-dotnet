using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;
using TimeProject.Infra.Bus;
using TimeProject.Infra.Data.Context;
using TimeProject.Infra.Identity.CommandHandlers;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Helpers;
using TimeProject.Infra.Identity.Interfaces;
using TimeProject.Infra.Identity.Services;

namespace TimeProject.Infra.IoC
{
    public class NativeInject
    {
        public static void InjectDependencies(IServiceCollection services)
        {

            services.AddMediatR(Assembly.Load("TimeProject.Infra.identity"), 
                                    Assembly.Load("TimeProject.Domain"));

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped<TenantyDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAuthHelper, UserAuthHelper>();
            services.AddScoped<IRequestHandler<RegisterUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<SignInUserCommand, bool>, UserCommandHandler>();

        }
    }
}
