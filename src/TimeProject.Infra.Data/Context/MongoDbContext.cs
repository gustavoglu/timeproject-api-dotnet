using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using TimeProject.Domain.Interfaces;

namespace TimeProject.Infra.Data.Context
{
    public abstract class MongoDbContext
    {

        public MongoClient Client;

        public MongoDbContext()
        {
            Configure();
            ConfigureConvetionsPack();
        }


        public virtual void ConfigureConvetionsPack()
        {
            var conventionPack = new ConventionPack {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                };

            ConventionRegistry.Register("camelCase", conventionPack, t => true);

        }


        public virtual void Configure()
        {
            var builder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddEnvironmentVariables()
              .Build();

            string connString = builder.GetConnectionString("MongoDB");

            MongoUrl mongoUrl = new MongoUrl(connString);
            Client = new MongoClient(mongoUrl);
        }

        public abstract IMongoDatabase GetDatabase();
    }
}
