using System.Collections.Generic;

namespace TimeProject.Application.ViewModels
{
    public class TeamFormViewModel
    {
        public TeamFormViewModel(string name, string teamleadUserId, List<DataSelect> usersOptions, List<DataSelect> customersOptions, List<DataSelect> projectsOptions)
        {
            Name = name;
            TeamleadUserId = teamleadUserId;
            UsersOptions = usersOptions ?? new List<DataSelect>();
            CustomersOptions = customersOptions ?? new List<DataSelect>();
            ProjectsOptions = projectsOptions ?? new List<DataSelect>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string TeamleadUserId { get; set; }
        public List<DataSelect> UsersOptions { get; set; }
        public List<DataSelect> CustomersOptions { get; set; }
        public List<DataSelect> ProjectsOptions { get; set; }
   
    }
}
