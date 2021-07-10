using System;
using TimeProject.Domain.Core.Events;

namespace TimeProject.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public DomainNotification(string type, string value)
        {
            Id = Guid.NewGuid().ToString();
            Version = 1;
            Type = type;
            Value = value;
        }

        public string Id { get; set; }
        public int Version { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
