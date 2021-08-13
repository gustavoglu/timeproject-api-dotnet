using System.Collections.Generic;

namespace TimeProject.Application.ViewModels
{
    public class TeamOptionsViewModel
    {
        public TeamOptionsViewModel(List<DataSelect> userOptions, List<DataSelect> customersOptions,
            List<DataSelect> projectOptions, List<KeyValuePair<string, string>> users)
        {
            UsersOptions = userOptions;
            CustomersOptions = customersOptions;
            ProjectsOptions = projectOptions;
            Users = users;
        }

        public List<DataSelect> UsersOptions { get; set; }
        public List<DataSelect> CustomersOptions { get; set; }
        public List<DataSelect> ProjectsOptions { get; set; }
        public List<KeyValuePair<string, string>> Users { get; set; }
    }
}
