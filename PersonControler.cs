using System;
using System.Linq;

namespace PersonalFinanceControlConsole
{
    internal class PersonControler
    {
        public PeopleDataBase PersonDataBase = new PeopleDataBase();
        public Person user;

        internal bool UserNotExists(int userId)
        {
            user = PersonDataBase.ReadUser(userId);
            return NotExists();
        }

        private bool NotExists()
        {
            if (user == null)
            {
                return true;
            }
            return false;
        }

        internal string GetUserName()
        {
            return user.Name;
        }

        internal bool AccountsEmpty()
        {
            return user.AccountsEmpty();
        }

        internal void InsertUser(int userId, string userName)
        {
            user = new Person(userId, userName);
            PersonDataBase.InsertUser(user);
        }

        internal bool VerifyExistingAccount(string accountName)
        {
            return user.VerifyExistingAccount(accountName);
        }

        internal void InsertAccount(string accountName)
        {
            int idAccount = 0;
            if (!user.AccountsEmpty())
            {
                idAccount = user.Accounts.Max(t => t.AccountId);
            }
            idAccount++;
            user.Accounts.Add(new Account(idAccount, accountName));
            PersonDataBase.UpdateAccounts(user);
        }

        internal void DeleteUser()
        {
            PersonDataBase.DeleteUser(user);
            var menuHandler = new Menus.MenuHandler();
            menuHandler.Show();
        }
    }
}