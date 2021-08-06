using TimeProject.Domain.Entities;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Infra.Data.Context;

namespace TimeProject.Infra.Data.Repositories
{
    public class TimeSheetRepository : Repository<TimeSheet>, ITimeSheetRepository
    {
        public TimeSheetRepository(TenantyDbContext context, IUserAuthHelper userAuthHelper) : base(context, userAuthHelper)
        {
        }
    }
}
