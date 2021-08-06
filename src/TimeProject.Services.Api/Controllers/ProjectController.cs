using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeProject.Domain.Commands.Projects;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces.Repositories;

namespace TimeProject.Services.Api.Controllers
{
    public class ProjectController : ApiControllerBase
    {
        private readonly IProjectRepository _repository;

        public ProjectController(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, IProjectRepository repository) : base(bus, notifications)
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
        public IActionResult Insert([FromBody]InsertProjectCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateProjectCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }


        [HttpDelete]
        public IActionResult Update(string id)
        {
            Bus.SendCommand(new DeleteProjectCommand() { Id = id});
            return ResponseDefault();
        }
    }
}
