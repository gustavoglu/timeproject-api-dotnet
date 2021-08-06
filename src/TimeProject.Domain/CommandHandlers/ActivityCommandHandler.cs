using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeProject.Domain.CommandHandlers;
using TimeProject.Domain.Commands.Activities;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces.Repositories;

namespace TimeActivity.Domain.CommandHandlers
{

    public class ActivityCommandHandler : CommandHandler, IRequestHandler<InsertActivityCommand, bool>, IRequestHandler<UpdateActivityCommand, bool>, IRequestHandler<DeleteActivityCommand, bool>
    {
        private readonly IActivityRepository _repository;

        public ActivityCommandHandler(IMediatorHandler bus, INotificationHandler<DomainNotification> domainNotificationHandler, IMapper mapper, IActivityRepository repository) : base(bus, domainNotificationHandler, mapper)
        {
            _repository = repository;
        }

        public Task<bool> Handle(InsertActivityCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<Activity>(request);
            _repository.Insert(entity);
            if (Notifications.HasNotifications()) return null;
            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<Activity>(request);
            _repository.Update(entity);
            if (Notifications.HasNotifications()) return Task.FromResult(false); 
            return Task.FromResult(true);

        }

        public Task<bool> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            _repository.Delete(request.Id);
            if (Notifications.HasNotifications()) return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}
