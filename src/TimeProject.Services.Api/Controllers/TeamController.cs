using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeProject.Domain.Commands.Teams;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;

namespace TimeProject.Services.Api.Controllers
{
    public class TeamController : ApiControllerBase
    {
        private readonly ITeamRepository _repository;

        public TeamController(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, ITeamRepository repository, IUserAuthHelper userAuthHelper) : base(bus, notifications, userAuthHelper)
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
        public IActionResult Insert([FromBody] InsertTeamCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateTeamCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }


        [HttpDelete]
        public IActionResult Update(string id)
        {
            Bus.SendCommand(new DeleteTeamCommand() { Id = id });
            return ResponseDefault();
        }
    }
}
