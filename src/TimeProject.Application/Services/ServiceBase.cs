using MediatR;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;

namespace TimeProject.Application.Services
{
    public class ServiceBase
    {
        protected readonly IUserAuthHelper UserAuthHelper;
        protected readonly IMediatorHandler Bus;
        protected readonly DomainNotificationHandler Notifications;
        public ServiceBase(IUserAuthHelper userAuthHelper, IMediatorHandler mediatorHandler,INotificationHandler<DomainNotification> notifications)
        {
            UserAuthHelper = userAuthHelper;
            Bus = mediatorHandler;
            Notifications = (DomainNotificationHandler)notifications;
        }
    }
}
