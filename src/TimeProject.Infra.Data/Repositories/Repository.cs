using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Domain.Core.Entities;
using TimeProject.Domain.Interfaces;
using TimeProject.Domain.Interfaces.Repositories;
using TimeProject.Domain.Pagination;
using TimeProject.Infra.Data.Context;

namespace TimeProject.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly TenantyDbContext Context;
        protected IMongoCollection<T> Collection;
        protected IUserAuthHelper UserAuthHelper;
        protected FilterDefinition<T> FilterDefault { get { return Builders<T>.Filter.Eq(e => e.IsDeleted, false); } }
        public Repository(TenantyDbContext context, IUserAuthHelper userAuthHelper)
        {
            Context = context;
            Collection = Context.GetDatabase().GetCollection<T>(typeof(T).Name.ToLower());
            UserAuthHelper = userAuthHelper;
        }
        public void Delete(string id)
        {
            var entity = GetById(id);
            SetDeleteEntityProperties(id, entity);
            Update(entity);
        }

        private void SetInsertEntityProperties(T entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.CreateBy = UserAuthHelper.GetUserName();
        }

        private void SetUpdateEntityProperties(string id, T entity)
        {
            var entityInDatabase = GetById(id);

            entity.CreateAt = entityInDatabase.CreateAt;
            entity.CreateBy = entityInDatabase.CreateBy;

            entity.UpdateAt = DateTime.Now;
            entity.UpdateBy = UserAuthHelper.GetUserName();
        }

        private void SetDeleteEntityProperties(string id, T entity)
        {
            var entityInDatabase = GetById(id);

            entity.CreateAt = entityInDatabase.CreateAt;
            entity.CreateBy = entityInDatabase.CreateBy;
            entity.UpdateAt = entityInDatabase.UpdateAt;
            entity.UpdateBy = entityInDatabase.UpdateBy;

            entity.DeleteAt = DateTime.Now;
            entity.DeleteBy = UserAuthHelper.GetUserName();
            entity.IsDeleted = true;
        }

        protected void SetPagination(IFindFluent<T, T> Find, int? page = null, int? limit = null)
        {
            if (Find == null || !(page.HasValue && limit.HasValue)) return;
            Find.Limit(limit.Value).Skip((page.Value - 1) * limit.Value);
        }

        protected void SetSort(IFindFluent<T, T> Find, string sortBy = null, bool sortDesc = false)
        {
            if (Find == null || string.IsNullOrEmpty(sortBy)) return;
            Find.Sort(sortDesc ? Builders<T>.Sort.Descending(sortBy) : Builders<T>.Sort.Ascending(sortBy));
        }

        public PaginationData<T> GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false)
        {
            IFindFluent<T, T> Find = Collection.Find(FilterDefault);
            long total = Find.CountDocuments();
            SetPagination(Find, page, limit);
            SetSort(Find, sortBy, sortDesc);
            return new PaginationData<T>(Find.ToList(), limit, page, total);

        }

        public T GetById(string id)
        {
            var idFilter = Builders<T>.Filter.Eq(e => e.Id, id);
            return Collection.Find(idFilter).FirstOrDefault();
        }

        public T Insert(T entity)
        {
            if (entity.Id == null) entity.Id = Guid.NewGuid().ToString();
            SetInsertEntityProperties(entity);
            Collection.InsertOne(entity);
            return GetById(entity.Id);
        }

        public PaginationData<T> Search(Expression<Func<T, bool>> predicate, int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false)
        {
            var expressoinFilter = Builders<T>.Filter.Where(predicate);

            IFindFluent<T, T> Find = Collection.Find(FilterDefault & expressoinFilter);
            long total = Find.CountDocuments();
            SetPagination(Find, page, limit);
            SetSort(Find, sortBy, sortDesc);
            return new PaginationData<T>(Find.ToList(), limit, page, total);
        }

        public T Update(T entity)
        {
            SetUpdateEntityProperties(entity.Id, entity);
            Collection.ReplaceOne(e => e.Id == entity.Id, entity);
            return GetById(entity.Id);
        }
    }
}
