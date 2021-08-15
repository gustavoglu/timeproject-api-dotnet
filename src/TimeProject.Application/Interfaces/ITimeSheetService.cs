using TimeProject.Domain.Entities;
using TimeProject.Domain.Pagination;

namespace TimeProject.Application.Interfaces
{
    public interface ITimeSheetService
    {
        PaginationData<TimeSheet> GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false);
    }
}
