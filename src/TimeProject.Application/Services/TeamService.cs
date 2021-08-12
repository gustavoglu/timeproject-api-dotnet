using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeProject.Domain.Commands.Teams;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Infra.Identity.Interfaces;

namespace TimeProject.Application.Services
{
    public class TeamService : ServiceBase
    {
        private readonly IUserService _userService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;

        public TeamService(IUserAuthHelper userAuthHelper, IMediatorHandler mediatorHandler,
                            INotificationHandler<DomainNotification> notifications, IUserService userService, ICustomerRepository customerRepository, IProjectRepository projectRepository) :
                            base(userAuthHelper, mediatorHandler, notifications)
        {
            _userService = userService;
            _customerRepository = customerRepository;
            _projectRepository = projectRepository;
        }

        private void AddClaimsInUsersByTeam(TeamCommand command)
        {
            var customers = _customerRepository.GetByIds(command.CustomerIds);
            var projecs = _projectRepository.GetByIds(command.ProjectIds);

            var customerClaims = from customer in customers select new Claim("customer", customer.Id);
            var projectClaims = from project in projecs select new Claim("project", project.Id);

            foreach (var userId in command.UserIds)
            {
                var claims = new List<Claim>();
                claims.AddRange(customerClaims);
                claims.AddRange(projectClaims);

                _userService.AddClaims(userId, claims);
            }
        }

        public void Insert(InsertTeamCommand command)
        {
            if (!command.IsValid()) return;

            Bus.SendCommand(command);

            if (Notifications.HasNotifications()) return;

            AddClaimsInUsersByTeam(command);

        }

        public void Upddate(UpdateTeamCommand command)
        {
            if (!command.IsValid()) return;

            Bus.SendCommand(command);

            if (Notifications.HasNotifications()) return;

            AddClaimsInUsersByTeam(command);
        }
    }
}
