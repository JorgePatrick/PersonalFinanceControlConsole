using MongoDB.Driver;
using MongoDB.Bson;
using System;

namespace PersonalFinanceControlConsole
{
    internal class Person
    {
        private int IdLogin;
        public string Name { get; set; }

        public Person(int idLogin, IMongoDatabase database)
        {
            this.IdLogin = idLogin;
            BsonDocument user = FindUser(idLogin, database);
            if (user != null)
            {

                this.Name = user["name"].AsString;
            }
        }
        private BsonDocument FindUser(int idLogin, IMongoDatabase database)
        {
            var collection = database.GetCollection<BsonDocument>("people");
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", idLogin);
            var userDocument = collection.Find(filter).FirstOrDefault();
            return userDocument;
        }
    }
}