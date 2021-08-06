using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeProject.Domain.Commands.Projects;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Entities;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces.Repositories;

namespace TimeProject.Domain.CommandHandlers
{

    public class ProjectCommandHandler : CommandHandler, IRequestHandler<InsertProjectCommand, bool>, IRequestHandler<UpdateProjectCommand, bool>, IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectRepository _repository;

        public ProjectCommandHandler(IMediatorHandler bus, INotificationHandler<DomainNotification> domainNotificationHandler, IMapper mapper, IProjectRepository repository) : base(bus, domainNotificationHandler, mapper)
        {
            _repository = repository;
        }

        public Task<bool> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<Project>(request);
            _repository.Insert(entity);
            if (Notifications.HasNotifications()) return null;
            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<Project>(request);
            _repository.Update(entity);
            if (Notifications.HasNotifications()) return Task.FromResult(false); 
            return Task.FromResult(true);

        }

        public Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            _repository.Delete(request.Id);
            if (Notifications.HasNotifications()) return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}
