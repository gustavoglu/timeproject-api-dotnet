using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Application.Interfaces;
using TimeProject.Domain.Core.Bus;
using TimeProject.Domain.Core.Notifications;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Domain.Pagination;
using TimeProject.Infra.Identity.Rules;

namespace TimeProject.Application.Services
{
    public class TimeSheetService : ServiceBase, ITimeSheetService
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        public TimeSheetService(IUserAuthHelper userAuthHelper, IMediatorHandler mediatorHandler, INotificationHandler<DomainNotification> notifications, ITimeSheetRepository timeSheetRepository) : base(userAuthHelper, mediatorHandler, notifications)
        {
            _timeSheetRepository = timeSheetRepository;
        }

        public PaginationData<TimeSheet> GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false)
        {
            string userName = UserAuthHelper.GetUserName();
            var claims = UserAuthHelper.GetClaims();
            var ruleClaims = claims.Where(claim => claim.Type == "rule").ToList();
            bool isMasterOrAdmin = ruleClaims.Exists(claim => claim.Value == ERule.Admin.ToString() || claim.Value == ERule.Master.ToString());
            if (isMasterOrAdmin) return _timeSheetRepository.GetAll(page, limit, sortBy, sortDesc);

            return _timeSheetRepository.Search(timeSheet => timeSheet.CreateBy == userName, page, limit, sortBy, sortDesc);
        }

    }
}
