using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeProject.Domain.Commands.Customers;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces.Repositories;

namespace TimeProject.Domain.CommandHandlers
{

    public class CustomerCommandHandler : CommandHandler, IRequestHandler<InsertCustomerCommand, bool>, IRequestHandler<UpdateCustomerCommand, bool>, IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public CustomerCommandHandler(IMediatorHandler bus, INotificationHandler<DomainNotification> domainNotificationHandler, IMapper mapper, ICustomerRepository repository) : base(bus, domainNotificationHandler, mapper)
        {
            _repository = repository;
        }

        public Task<bool> Handle(InsertCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<Customer>(request);
            _repository.Insert(entity);
            if (Notifications.HasNotifications()) return null;
            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            var entity = Mapper.Map<Customer>(request);
            _repository.Update(entity);
            if (Notifications.HasNotifications()) return Task.FromResult(false); 
            return Task.FromResult(true);

        }

        public Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!CommandIsValid(request)) return Task.FromResult(false);
            _repository.Delete(request.Id);
            if (Notifications.HasNotifications()) return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}
