using MongoDB.Driver;
using TimeProject.Domain.Interfaces;

namespace TimeProject.Infra.Data.Context
{
    public class TenantyDbContext : MongoDbContext
    {
        private readonly IUserAuthHelper _userAuthHelper;
        public TenantyDbContext(IUserAuthHelper userAuthHelper)
        {
            this._userAuthHelper = userAuthHelper;
        }

        public override IMongoDatabase GetDatabase()
        {
            string database = _userAuthHelper.GetTenanty();
            if (string.IsNullOrEmpty(database)) return null;
            return Client.GetDatabase(database);
        }
    }
}