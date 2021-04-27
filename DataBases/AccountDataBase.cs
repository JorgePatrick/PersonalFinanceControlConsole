using MongoDB.Bson;
using MongoDB.Driver;
using PersonalFinanceControlConsole.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.DataBases
{
    class AccountDataBase : DataBase
    {
        protected IMongoCollection<Person> Collection;

        public AccountDataBase()
        {
            Collection = Database.GetCollection<Person>("people");
        }

        internal void UpdateAccounts(List<Account> accounts, BsonObjectId idPerson)
        {
            var filter = Builders<Person>.Filter.Eq("_id", idPerson);
            var value = accounts;
            var fieldDefinitionAccounts = "accounts";
            var update = Builders<Person>.Update.Set(fieldDefinitionAccounts, value);
            Collection.UpdateOne(filter, update);
        }
    }
}
