using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;

namespace TimeProject.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]

    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly IMediatorHandler Bus;
        protected readonly DomainNotificationHandler Notifications;
        protected readonly IUserAuthHelper UserAuthHelper;

        protected ApiControllerBase(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, IUserAuthHelper userAuthHelper)
        {
            Bus = bus;
            Notifications = (DomainNotificationHandler)notifications;
            UserAuthHelper = userAuthHelper;
        }




        protected IActionResult ResponseDefault(object obj = null)
        {
            SendNotificationsByModelStateErrors();

            if (!Notifications.HasNotifications())
                return Ok(new { Success = true, Data = obj });

            var errors = Notifications.Notifications
                                        .Select(notification =>
                                        new KeyValuePair<string, string>(notification.Type, notification.Value));

            return BadRequest(new { Success = false, Data = errors });
        }

        private void SendNotificationsByModelStateErrors()
        {
            if (ModelState.IsValid) return;
            var notifications = ModelState.SelectMany(ms => ms.Value.Errors)
                        .Select(error => new DomainNotification("ModelState", error.ErrorMessage));
            foreach (var notification in notifications)
                Bus.RaiseEvent(notification);
        }
    }
}
