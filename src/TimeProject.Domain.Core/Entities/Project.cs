using System;

namespace TimeProject.Domain.Core.Entities
{
    public class Project : Entity
    {
        public Project(string name, string customerId, string color = null, string code = null, string description = null, 
                        int order = 1, DateTime? orderDate = null, DateTime? startDate = null, DateTime? endDate = null, double budget = 0)
        {
            Name = name;
            Color = color;
            Code = code;
            Description = description;
            CustomerId = customerId;
            Order = order;
            OrderDate = orderDate;
            StartDate = startDate;
            EndDate = endDate;
            Budget = budget;
        }

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
