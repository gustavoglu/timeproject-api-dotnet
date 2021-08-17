using System;
using System.Collections.Generic;
using TimeProject.Domain.Commands.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Domain.Commands.TimeSheets
{
    public class TimeSheetCommand : EntityCommand
    {
        public TimeSheetCommand()
        {
            Tags = new List<Tag>();
        }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CustomerId { get; set; }
        public string ProjectId { get; set; }
        public string ActivityId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public bool Billable { get; set; } = true;
        public double? FixedRate { get; set; }
        public double? HourlyRate { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
