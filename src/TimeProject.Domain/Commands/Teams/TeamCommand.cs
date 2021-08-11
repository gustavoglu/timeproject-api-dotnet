using TimeProject.Domain.Commands.Entities;

namespace TimeProject.Domain.Commands.Teams
{
    public class TeamCommand : EntityCommand
    {
        public string Name { get; set; }
        public string TeamleadUserId { get; set; }
        public string[] UserIds { get; set; }
        public string[] ProjectIds { get; set; }
        public string[] CustomerIds { get; set; }
    }
}
