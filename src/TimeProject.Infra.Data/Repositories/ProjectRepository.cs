using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Domain.Interfaces;
using TimeProject.Infra.Data.Context;
using TimeProject.Domain.Core.Entities;
using TimeProject.Domain.Entities;

namespace TimeProject.Infra.Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(TenantyDbContext context, IUserAuthHelper userAuthHelper) : base(context, userAuthHelper)
        {
        }
    }
}
