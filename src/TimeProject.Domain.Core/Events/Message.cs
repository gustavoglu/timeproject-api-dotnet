using MediatR;

namespace TimeProject.Domain.Core.Events
{
    public abstract class Message : IRequest<bool>
    {
        protected Message(string aggregateId = null, string messageType = null)
        {
            AggregateId = aggregateId;
            MessageType = messageType ?? this.GetType().Name;
        }

        private string AggregateId { get; set; }
        private string MessageType { get; set; }

    }
}
