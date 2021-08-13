using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeProject.Application.Interfaces;
using TimeProject.Application.ViewModels;
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
        private readonly ITeamService _teamService;

        public TeamController(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, ITeamRepository repository, IUserAuthHelper userAuthHelper, ITeamService teamService) : base(bus, notifications, userAuthHelper)
        {
            _repository = repository;
            _teamService = teamService;
        }


        [HttpGet("options")]
        public IActionResult GetOptions(string teamId = null)
        {
            return ResponseDefault(_teamService.GetTeamOptions(teamId));
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
        public IActionResult Insert([FromBody] TeamFormViewModel model)
        {
            _teamService.Insert(model);
            return ResponseDefault();
        }

        [HttpPut]
        public IActionResult Update([FromBody] TeamFormViewModel model)
        {
            _teamService.Update(model);
            return ResponseDefault();
        }


        [HttpDelete]
        public IActionResult Delete(string id)
        {
            Bus.SendCommand(new DeleteTeamCommand() { Id = id });
            return ResponseDefault();
        }
    }
}
