using MongoDB.Driver;
using PersonalFinanceControlConsole.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.DataBases
{
    abstract class DataBase
    {
        protected MongoClient DbClient;
        protected IMongoDatabase Database;

        public DataBase()
        {
            DbClient = new MongoClient("mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb");
            Database = DbClient.GetDatabase("personal_finance");
        }
    }
}
