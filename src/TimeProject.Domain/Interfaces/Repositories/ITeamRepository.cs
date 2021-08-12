using System.Collections.Generic;
using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Interfaces.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        IEnumerable<Customer> GetCustomersInTeamsByUserId(string userId);
        IEnumerable<Project> GetProjectsInTeamsByUserId(string userId);
    }
}
