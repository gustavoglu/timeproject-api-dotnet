using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeProject.Application.Interfaces;
using TimeProject.Domain.Commands.TimeSheets;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Services.Api.Controllers;

namespace TimeTimeSheet.Services.Api.Controllers
{
    public class TimeSheetController : ApiControllerBase
    {
        private readonly ITimeSheetRepository _repository;
        private readonly ITimeSheetService _timeSheetService;
        private readonly ITeamRepository _teamRepository;
        private readonly IActivityRepository _activityRepository;


        public TimeSheetController(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications,
            ITimeSheetRepository repository, ITeamRepository teamRepository, IActivityRepository activityRepository, IUserAuthHelper userAuthHelper, ITimeSheetService timeSheetService) : base(bus, notifications, userAuthHelper)
        {
            _repository = repository;
            _teamRepository = teamRepository;
            _activityRepository = activityRepository;
            _timeSheetService = timeSheetService;
        }


        [HttpGet]
        public IActionResult GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false)
        {
            return ResponseDefault(_timeSheetService.GetAll(page, limit, sortBy, sortDesc));
        }


        [HttpGet("options")]
        public IActionResult GetOptions()
        {
            var customers = _teamRepository.GetCustomersInTeamsByUserId(UserAuthHelper.GetId());
            var projects = _teamRepository.GetProjectsInTeamsByUserId(UserAuthHelper.GetId());
            var activities = _activityRepository.GetAll().Data;
            return ResponseDefault(new { customers, projects, activities });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return ResponseDefault(_repository.GetById(id));
        }

        [HttpPost]
        public IActionResult Insert([FromBody]InsertTimeSheetCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateTimeSheetCommand command)
        {
            Bus.SendCommand(command);
            return ResponseDefault();
        }


        [HttpDelete]
        public IActionResult Delete(string id)
        {
            Bus.SendCommand(new DeleteTimeSheetCommand() { Id = id});
            return ResponseDefault();
        }
    }
}
