using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TimeProject.Infra.Identity.Models
{
    [BsonIgnoreExtraElements]
    public class User : MongoUser<string>
    {
        public User(string email,string tenanty, string name,bool masterUser = false)
        {
            Id = this.Id ?? Guid.NewGuid().ToString();
            Email = email;
            UserName = email;
            Tenanty = tenanty;
            Name = name;
            MasterUser = masterUser;
        }

        public string Name { get; set; }
        public string Tenanty { get; set; }
        public bool MasterUser { get; set; }
    }
}
