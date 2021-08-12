using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.Domain.Commands.Activities;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Services.Api.Controllers;

namespace TimeActivity.Services.Api.Controllers
{
    [Authorize]
    public class ActivityController : ApiControllerBase
    {
        private readonly IActivityRepository _repository;

        public ActivityController(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, IActivityRepository repository, IUserAuthHelper userAuthHelper) : base(bus, notifications, userAuthHelper)
        {
            _repository = repository;
        }


        [HttpGet]
        public IActionResult GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false)
        {
            return ResponseDefault(_repository.GetAll(page, limit, sortBy, sortDesc));
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return ResponseDefault(_repository.GetById(id));
        }

        [HttpPost]
        public IActionResult Insert([FromBody]InsertActivityCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateActivityCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }


        [HttpDelete]
        public IActionResult Update(string id)
        {
            Bus.SendCommand(new DeleteActivityCommand() { Id = id});
            return ResponseDefault();
        }
    }
}
