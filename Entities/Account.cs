using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PersonalFinanceControlConsole.Entities
{
    public class Account
    {
        [BsonElement("account_id")]
        public int AccountId { get; set; }
        [BsonElement("account_name")]
        public string AccountName { get; set; }
        public Account(int accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }

        internal string[,] GetInfos()
        {
            string[,] infos = 
                {
                    { "Name", AccountName }
                };
            return infos;
        }
    }
}