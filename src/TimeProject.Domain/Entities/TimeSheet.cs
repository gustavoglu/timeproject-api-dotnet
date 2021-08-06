using System;
using System.Collections.Generic;
using TimeProject.Domain.Core.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Domain.Entities
{
    public class TimeSheet : Entity
    {
        public TimeSheet(DateTime startDate, string projectId, string activityId, string userId,string code = null, DateTime? endDate = null, string customerId = null, 
                             string description = null, bool billable = true, double? fixedRate = null, 
                            double? hourlyRate = null, IEnumerable<Tag> tags = null)
        {
            StartDate = startDate;
            EndDate = endDate;
            Code = code;
            CustomerId = customerId;
            ProjectId = projectId;
            ActivityId = activityId;
            Description = description;
            UserId = userId;
            Billable = billable;
            FixedRate = fixedRate;
            HourlyRate = hourlyRate;
            Tags = tags ?? new List<Tag>() ;
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
