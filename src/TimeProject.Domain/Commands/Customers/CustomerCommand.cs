using System;
using TimeProject.Domain.Commands.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Domain.Commands.Customers
{
    public abstract class CustomerCommand : EntityCommand
    {
        public CustomerCommand()
        {
            Location = new Location();
            Contact = new Contact();
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public Location Location { get; set; }
        public Contact Contact { get; set; }
        public double Budget { get; set; }
        public TimeSpan TimeBudget { get; set; }

    }
}
