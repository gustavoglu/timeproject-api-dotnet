using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeProject.Domain.CommandHandlers;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Infra.Identity.Commands;
using TimeProject.Infra.Identity.Interfaces;

namespace TimeProject.Infra.Identity.CommandHandlers
{
    public class UserCommandHandler : CommandHandler, IRequestHandler<RegisterUserCommand, bool>, IRequestHandler<SignInUserCommand, bool>
    {
        private readonly IUserService _userService;
        public UserCommandHandler(IMediatorHandler bus, INotificationHandler<DomainNotification> domainNotificationHandler, IUserService userService) : base(bus, domainNotificationHandler)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return false;
            await _userService.RegisterAsync(request);
            return Notifications.HasNotifications();
        }

        public async Task<bool> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return false;
            await _userService.SignInAsync(request);
            return Notifications.HasNotifications();
        }
    }
}
