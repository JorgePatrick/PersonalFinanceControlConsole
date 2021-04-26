using MongoDB.Bson;
using MongoDB.Driver;
using PersonalFinanceControlConsole.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.DataBases
{
    class AccountDataBase
    {
        protected MongoClient DbClient;
        protected IMongoDatabase Database;
        protected IMongoCollection<Person> PeopleCollection;

        public AccountDataBase()
        {
            DbClient = new MongoClient("mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb");
            Database = DbClient.GetDatabase("personal_finance");
            PeopleCollection = Database.GetCollection<Person>("people");
        }
        internal void UpdateAccounts(List<Account> accounts, BsonObjectId idPerson)
        {
            var filter = Builders<Person>.Filter.Eq("_id", idPerson);
            var value = accounts;
            var fieldDefinitionAccounts = "accounts";
            var update = Builders<Person>.Update.Set(fieldDefinitionAccounts, value);
            PeopleCollection.UpdateOne(filter, update);
        }
    }
}
