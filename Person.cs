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

        public Person(int userId)
        {
            this.UserId = userId;
            this.Accounts = new List<Account>();
        }

        public Person(int userId, string userName) : this(userId)
        {
            Name = userName;
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