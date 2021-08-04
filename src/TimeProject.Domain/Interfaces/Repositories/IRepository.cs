using System;
using System.Linq.Expressions;
using TimeProject.Domain.Core.Entities;
using TimeProject.Domain.Pagination;

namespace TimeProject.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        PaginationData<T> GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false);
        PaginationData<T> Search(Expression<Func<T, bool>> predicate, int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false);
        T GetById(string id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(string id);

    }
}
