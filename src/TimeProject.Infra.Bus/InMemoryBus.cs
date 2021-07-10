using MediatR;
using System.Threading.Tasks;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Commands;
using TimeProject.Domain.Core.Events;

namespace TimeProject.Infra.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public InMemoryBus(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public async Task RaiseEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);

        }

        public async Task SendCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }
    }
}
