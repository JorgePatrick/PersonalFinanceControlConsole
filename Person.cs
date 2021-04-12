using MongoDB.Driver;
using MongoDB.Bson;
using System;

namespace PersonalFinanceControlConsole
{
    internal class Person
    {
        public int IdLogin { get; set; }
        public string Name { get; set; }
        public IMongoCollection<BsonDocument> PeopleCollection;

        public Person(int idLogin, IMongoDatabase database)
        {
            this.IdLogin = idLogin;
            this.PeopleCollection = database.GetCollection<BsonDocument>("people"); ;
            BsonDocument user = FindUser();
            if (user != null)
            {
                this.Name = user["name"].AsString;
            }
        }

        internal void Save()
        {
            var document = new BsonDocument
            {
                { "user_id", IdLogin },
                { "name", Name }
            };
            PeopleCollection.InsertOne(document);
            
        }

        private BsonDocument FindUser()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", IdLogin);
            var userDocument = PeopleCollection.Find(filter).FirstOrDefault();
            return userDocument;
        }

        internal void Delete()
        {
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("user_id", IdLogin);
            PeopleCollection.DeleteOne(deleteFilter);
        }

        internal bool VerifyExistingAccount(string accountName)
        {
            throw new NotImplementedException();
        }
    }
}