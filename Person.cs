using System;
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
        [BsonIgnore]
        public IMongoCollection<Person> PeopleCollection;

        internal bool VerifyExistingAccount(string accountName)
        {
            throw new NotImplementedException();
        }
        internal bool NotExists()
        {
            return string.IsNullOrEmpty(Name);
        }


    }
}