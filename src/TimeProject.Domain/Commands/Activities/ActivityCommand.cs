using System;
using TimeProject.Domain.Commands.Entities;

namespace TimeProject.Domain.Commands.Activities
{
    public class ActivityCommand : EntityCommand
    {
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
