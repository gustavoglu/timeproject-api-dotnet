﻿using System;
using TimeProject.Domain.Core.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(string name, string code = null, string description = null, string companyName = null, 
                            Location location = null, Contact contact = null, double budget = 0, string timeBudget = null)
        {
            Name = name;
            Code = code;
            Description = description;
            CompanyName = companyName;
            Location = location ?? new Location();
            Contact = contact ?? new Contact();
            Budget = budget;
            TimeBudget = timeBudget;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public Location Location { get; set; }
        public Contact Contact { get; set; }
        public double Budget { get; set; }
        public string TimeBudget { get; set; }
    }
}
