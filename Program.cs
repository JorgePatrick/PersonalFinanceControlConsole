using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace PersonalFinanceControlConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb");
            var database = dbClient.GetDatabase("personal_finance");

            Menu.Login(database);
        }
    }
}
