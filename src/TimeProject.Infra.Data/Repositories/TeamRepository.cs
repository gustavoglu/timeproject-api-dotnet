using MongoDB.Driver;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Domain.Pagination;
using TimeProject.Infra.Data.Context;

namespace TimeProject.Infra.Data.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        public TeamRepository(TenantyDbContext context, IUserAuthHelper userAuthHelper, ICustomerRepository customerRepository, IProjectRepository projectRepository) : base(context, userAuthHelper)
        {
            _customerRepository = customerRepository;
            _projectRepository = projectRepository;
        }

        public IEnumerable<Team> GetTeamsByUserId(string userId)
        {
            var userIdFilter = Builders<Team>.Filter.AnyEq(team => team.UserIds, userId);
            var teamleadUserIdFilter = Builders<Team>.Filter.Eq(team => team.TeamleadUserId, userId);
            var orFilter = Builders<Team>.Filter.Or(userIdFilter, teamleadUserIdFilter);

            IFindFluent<Team, Team> Find = Collection.Find(FilterDefault & orFilter);

            return Find.ToList().Distinct().ToList();
        }

        public IEnumerable<Customer> GetCustomersInTeamsByUserId(string userId)
        {
            var teams = GetTeamsByUserId(userId);
            var customerIds = teams.SelectMany(team => team.CustomerIds).ToArray();
            return _customerRepository.GetByIds(customerIds);
        }

        public IEnumerable<Project> GetProjectsInTeamsByUserId(string userId)
        {
            var teams = GetTeamsByUserId(userId);
            var projectIds = teams.SelectMany(team => team.ProjectIds).ToArray();
            return _projectRepository.GetByIds(projectIds);
        }
    }
}
