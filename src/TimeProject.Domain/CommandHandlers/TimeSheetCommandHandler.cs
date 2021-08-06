using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeProject.Domain.CommandHandlers;
using TimeProject.Domain.Commands.TimeSheets;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces.Repositories;

namespace TimeTimeSheet.Domain.CommandHandlers
{

    public class TimeSheetCommandHandler : CommandHandler, IRequestHandler<InsertTimeSheetCommand, bool>, IRequestHandler<UpdateTimeSheetCommand, bool>, IRequestHandler<DeleteTimeSheetCommand, bool>
    {
        private readonly ITimeSheetRepository _repository;

        public TimeSheetCommandHandler(IMediatorHandler bus, INotificationHandler<DomainNotification> domainNotificationHandler, IMapper mapper, ITimeSheetRepository repository) : base(bus, domainNotificationHandler, mapper)
        {
            _repository = repository;
        }

        public Task<bool> Handle(InsertTimeSheetCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<TimeSheet>(request);
            _repository.Insert(entity);
            if (Notifications.HasNotifications()) return null;
            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateTimeSheetCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<TimeSheet>(request);
            _repository.Update(entity);
            if (Notifications.HasNotifications()) return Task.FromResult(false); 
            return Task.FromResult(true);

        }

        public Task<bool> Handle(DeleteTimeSheetCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            _repository.Delete(request.Id);
            if (Notifications.HasNotifications()) return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}
