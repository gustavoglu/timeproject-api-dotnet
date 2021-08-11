using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeProject.Domain.CommandHandlers;
using TimeProject.Domain.Commands.Teams;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces.Repositories;

namespace TimeProject.Domain.CommandHandlers
{

    public class TeamCommandHandler : CommandHandler, IRequestHandler<InsertTeamCommand, bool>, IRequestHandler<UpdateTeamCommand, bool>, IRequestHandler<DeleteTeamCommand, bool>
    {
        private readonly ITeamRepository _repository;

        public TeamCommandHandler(IMediatorHandler bus, INotificationHandler<DomainNotification> domainNotificationHandler, IMapper mapper, ITeamRepository repository) : base(bus, domainNotificationHandler, mapper)
        {
            _repository = repository;
        }

        public Task<bool> Handle(InsertTeamCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<Team>(request);
            _repository.Insert(entity);
            if (Notifications.HasNotifications()) return null;
            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<Team>(request);
            _repository.Update(entity);
            if (Notifications.HasNotifications()) return Task.FromResult(false); 
            return Task.FromResult(true);

        }

        public Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            _repository.Delete(request.Id);
            if (Notifications.HasNotifications()) return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}
