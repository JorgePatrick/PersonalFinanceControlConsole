using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using MongoDB.Driver;
using PersonalFinanceControlConsole.Entities;

namespace PersonalFinanceControlConsole.DataBases
{
    internal class PeopleDataBase : DataBase
    {
        protected IMongoCollection<Person> Collection;

        public PeopleDataBase()
        {
            Collection = Database.GetCollection<Person>("people");
        }

        internal Person ReadUser(int userId)
        {
            var query =
                from e in Collection.AsQueryable<Person>()
                where e.UserId == userId
                select e;
            var user = query.FirstOrDefault();
            return user;
        }

        internal void InsertUser(Person user)
        {
            Collection.InsertOne(user);
        }

        internal void DeleteUser(Person user)
        {
            var filter = Builders<Person>.Filter.Eq("_id", user.Id);
            Collection.DeleteOne(filter);
        }

        internal void UpdateName(Person user)
        {
            var filter = Builders<Person>.Filter.Eq("_id", user.Id);
            var value = user.Name;
            var fieldDefinitionAccounts = "name";
            var update = Builders<Person>.Update.Set(fieldDefinitionAccounts, value);
            Collection.UpdateOne(filter, update);
        }
    }
}