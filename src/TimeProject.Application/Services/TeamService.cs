using MediatR;
using System.Collections.Generic;
using System.Linq;
using TimeProject.Application.Interfaces;
using TimeProject.Application.ViewModels;
using TimeProject.Domain.Commands.Teams;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Infra.Identity.Interfaces;

namespace TimeProject.Application.Services
{
    public class TeamService : ServiceBase, ITeamService
    {
        private readonly IUserService _userService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITeamRepository _teamRepository;

        public TeamService(IUserAuthHelper userAuthHelper, IMediatorHandler mediatorHandler,
                            INotificationHandler<DomainNotification> notifications, IUserService userService, ICustomerRepository customerRepository, IProjectRepository projectRepository, ITeamRepository teamRepository) :
                            base(userAuthHelper, mediatorHandler, notifications)
        {
            _userService = userService;
            _customerRepository = customerRepository;
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
        }

        public void Insert(TeamFormViewModel model)
        {
            var userIds = model.UsersOptions.Where(user => user.Select).Select(user => user.Key).ToArray();
            var projectIds = model.ProjectsOptions.Where(project => project.Select).Select(user => user.Key).ToArray();
            var customerIds = model.CustomersOptions.Where(customer => customer.Select).Select(user => user.Key).ToArray();

            var command = new InsertTeamCommand()
            {
                Name = model.Name,
                TeamleadUserId = model.TeamleadUserId,
                UserIds = userIds,
                ProjectIds = projectIds,
                CustomerIds = customerIds
            };

            Bus.SendCommand(command);
        }

        public void Update(TeamFormViewModel model)
        {
            var userIds = model.UsersOptions.Where(user => user.Select).Select(user => user.Key).ToArray();
            var projectIds = model.ProjectsOptions.Where(project => project.Select).Select(user => user.Key).ToArray();
            var customerIds = model.CustomersOptions.Where(customer => customer.Select).Select(user => user.Key).ToArray();

            var command = new UpdateTeamCommand()
            {
                Id = model.Id,
                Name = model.Name,
                TeamleadUserId = model.TeamleadUserId,
                UserIds = userIds,
                ProjectIds = projectIds,
                CustomerIds = customerIds
            };

            Bus.SendCommand(command);
        }

        public TeamOptionsViewModel GetTeamOptions(string teamId = null)
        {

            List<DataSelect> usersTeamDataSelect;
            List<DataSelect> customersDataSelect;
            List<DataSelect> projectsDataSelect;

            var users = _userService.GetAll();
            var customers = _customerRepository.GetAll().Data;
            var projects = _projectRepository.GetAll().Data;
            List<KeyValuePair<string, string>> usersDataSelect = users.Select(user => new KeyValuePair<string, string>(user.Id, $"{user.Name} ({user.Email})")).ToList();
            if (teamId != null)
            {
                var team = _teamRepository.GetById(teamId);
                usersTeamDataSelect = users.Select(user => new DataSelect(user.Id, user.Name, team.UserIds.ToList().Exists(userId => userId == user.Id))).ToList();
                customersDataSelect = customers.Select(customer => new DataSelect(customer.Id, customer.Name, team.CustomerIds.ToList().Exists(customerId => customerId == customer.Id))).ToList();
                projectsDataSelect = projects.Select(project => new DataSelect(project.Id, project.Name, team.ProjectIds.ToList().Exists(projectId => projectId == project.Id))).ToList();
            }
            else
            {
                usersTeamDataSelect = users.Select(user => new DataSelect(user.Id, user.Name)).ToList();
                customersDataSelect = customers.Select(customer => new DataSelect(customer.Id, customer.Name)).ToList();
                projectsDataSelect = projects.Select(project => new DataSelect(project.Id, project.Name)).ToList();
            }

            return new TeamOptionsViewModel(usersTeamDataSelect, customersDataSelect, projectsDataSelect, usersDataSelect);

        }


    }
}
