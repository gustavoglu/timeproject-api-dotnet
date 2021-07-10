using System.Threading.Tasks;
using TimeProject.Domain.Core.Commands;
using TimeProject.Domain.Core.Events;

namespace TimeProject.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;


    }
}
