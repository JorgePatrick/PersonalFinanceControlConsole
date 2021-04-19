using MongoDB.Bson.Serialization.Attributes;

namespace PersonalFinanceControlConsole
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
    }
}