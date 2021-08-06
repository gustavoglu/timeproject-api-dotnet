using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeProject.Domain.Commands.TimeSheets;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Services.Api.Controllers;

namespace TimeTimeSheet.Services.Api.Controllers
{
    public class TimeSheetController : ApiControllerBase
    {
        private readonly ITimeSheetRepository _repository;

        public TimeSheetController(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, ITimeSheetRepository repository) : base(bus, notifications)
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
        public IActionResult Update(string id)
        {
            Bus.SendCommand(new DeleteTimeSheetCommand() { Id = id});
            return ResponseDefault();
        }
    }
}
