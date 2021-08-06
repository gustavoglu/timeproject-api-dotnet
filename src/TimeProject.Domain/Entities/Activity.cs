using System;
using TimeProject.Domain.Core.Entities;

namespace TimeProject.Domain.Entities
{
    public class Activity : Entity
    {
        public Activity(string name, string code = null, string color = null, string description = null,
                                    string customerId = null, string projectId = null, double budget = 0, TimeSpan? timeBudget = null)
        {
            Name = name;
            Color = color;
            Description = description;
            CustomerId = customerId;
            ProjectId = projectId;
            Budget = budget;
            TimeBudget = timeBudget ?? TimeSpan.Zero;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string ProjectId { get; set; }
        public double Budget { get; set; }
        public TimeSpan TimeBudget { get; set; }
    }
}
