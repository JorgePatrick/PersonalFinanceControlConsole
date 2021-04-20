using MongoDB.Driver;
using System;
using System.Linq;

namespace PersonalFinanceControlConsole
{
    internal class Controler
    {
        internal static bool UserNotExists(int userId)
        {
            var user = ReadUser(userId);
            return NotExists(user);
        }

        private static bool NotExists(Person user)
        {
            if (user == null)
            {
                return true;
            }
            return false;
        }

        internal static string GetUserName(int userId)
        {
            var user = ReadUser(userId);
            return user.Name;
        }

        internal static bool AccountsEmpty(int userId)
        {
            var user = ReadUser(userId);
            return user.AccountsEmpty();
        }

        internal static bool VerifyExistingAccount(int userId, string accountName)
        {
            var user = ReadUser(userId);
            return user.VerifyExistingAccount(accountName);
        }
        internal static void InsertAccount(int userId, string accountName)
        {
            var user = ReadUser(userId);
            int idAccount = 0;
            if (!user.AccountsEmpty())
            {
                idAccount = user.Accounts.Max(t => t.AccountId);
            }
            idAccount++;
            user.Accounts.Add(new Account(idAccount, accountName));
            UpdateAccounts(user);
        }

        internal static Person ReadUser(int userId)
        {
            var user = new Person(userId);
            var query =
                from e in user.Collection.AsQueryable<Person>()
                where e.UserId == userId
                select e;
            user = query.FirstOrDefault();
            return user;
        }

        internal static void InsertUser(int userId, string userName)
        {
            Person user = new Person(userId, userName);
            user.Collection.InsertOne(user);
        }

        internal static void DeleteUser(int userId)
        {
            var user = ReadUser(userId);
            var filter = Builders<Person>.Filter.Eq("_id", user.Id);
            user.Collection.DeleteOne(filter);
            MenuHandler.Show();
        }

        private static void UpdateAccounts(Person user)
        {
            var filter = Builders<Person>.Filter.Eq("_id", user.Id);
            var update = Builders<Person>.Update.Set("accounts", user.Accounts);
            user.Collection.UpdateOne(filter, update);
        }
    }
}