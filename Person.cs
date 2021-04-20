using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace PersonalFinanceControlConsole
{
    internal class Person
    {
        [BsonId]
        public BsonObjectId Id { get; set; }
        [BsonElement("user_id")]
        public int UserId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("accounts")]
        public List<Account> Accounts { get; set; }
        [BsonIgnore]
        public IMongoCollection<Person> PeopleCollection;

        internal bool NotExists()
        {
            return string.IsNullOrEmpty(Name);
        }

        internal bool VerifyExistingAccount(string accountName)
        {
            return Accounts.Any(x => x.AccountName == accountName);
        }

        internal bool AccountsEmpty()
        {
            return Accounts.Count == 0;
        }
    }
}