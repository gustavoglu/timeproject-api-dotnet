using TimeProject.Domain.Core.Entities;

namespace TimeProject.Domain.Entities
{
    public class Team : Entity
    {
        public Team(string name, string teamleadUserId, string[] userIds, string[] projectIds = null, string[] customerIds = null)
        {
            Name = name;
            TeamleadUserId = teamleadUserId;
            UserIds = userIds;
            ProjectIds = projectIds ?? new string[] { };
            CustomerIds = customerIds ?? new string[] { };
        }

        public string Name { get; set; }
        public string TeamleadUserId { get; set; }
        public string[] UserIds { get; set; }
        public string[] ProjectIds { get; set; }
        public string[] CustomerIds { get; set; }

    }
}
