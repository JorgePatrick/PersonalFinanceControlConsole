using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using MongoDB.Driver;
using PersonalFinanceControlConsole.Entities;

namespace PersonalFinanceControlConsole.DataBases
{
    internal class PeopleDataBase
    {
        protected MongoClient DbClient;
        protected IMongoDatabase Database;
        protected IMongoCollection<Person> PeopleCollection;

        public PeopleDataBase()
        {
            DbClient = new MongoClient("mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb");
            Database = DbClient.GetDatabase("personal_finance");
            PeopleCollection = Database.GetCollection<Person>("people");
        }

        internal Person ReadUser(int userId)
        {
            var user = new Person(userId);
            var query =
                from e in PeopleCollection.AsQueryable<Person>()
                where e.UserId == userId
                select e;
            user = query.FirstOrDefault();
            return user;
        }

        internal void InsertUser(Person user)
        {
            PeopleCollection.InsertOne(user);
        }

        internal void DeleteUser(Person user)
        {
            var filter = Builders<Person>.Filter.Eq("_id", user.Id);
            PeopleCollection.DeleteOne(filter);
        }
    }
}