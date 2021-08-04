using AutoMapper;
using MediatR;
using System.Linq;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Commands;
using TimeProject.Domain.Core.Notifications;

namespace TimeProject.Domain.CommandHandlers
{
    public abstract class CommandHandler
    {
        protected readonly IMediatorHandler Bus;
        protected readonly DomainNotificationHandler Notifications;
        protected readonly IMapper Mapper;
        public CommandHandler(IMediatorHandler bus, INotificationHandler<DomainNotification> domainNotificationHandler, IMapper mapper)
        {
            Bus = bus;
            this.Notifications = (DomainNotificationHandler)domainNotificationHandler;
            Mapper = mapper;
        }

        protected bool CommandIsValid<T>(T command) where T : Command
        {
            if (command.IsValid()) return true;

            var domainNotifications = command.ValidationResult.Errors
                                                .Select(error => new DomainNotification(error.PropertyName, error.ErrorMessage));

            foreach (var domainNotificatoin in domainNotifications)
                Bus.RaiseEvent(domainNotificatoin);


            return false;
        }
    }
}
