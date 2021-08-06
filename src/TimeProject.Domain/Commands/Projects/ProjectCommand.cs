using System;
using TimeProject.Domain.Commands.Entities;

namespace TimeProject.Domain.Commands.Projects
{
    public abstract class ProjectCommand : EntityCommand
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public int Order { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Budget { get; set; }
    }
}
