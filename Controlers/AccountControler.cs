using MongoDB.Bson;
using PersonalFinanceControlConsole.DataBases;
using PersonalFinanceControlConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalFinanceControlConsole.Controlers
{
    class AccountControler
    {
        public AccountDataBase AccountDataBase = new AccountDataBase();
        public List<Account> Accounts;

        internal bool AccountsEmpty()
        {
            return Accounts.Count == 0;
        }

        internal bool VerifyExistingAccount(string accountName)
        {
            return Accounts.Any(x => x.AccountName == accountName);
        }

        internal void InsertAccount(BsonObjectId idPerson, string accountName)
        {
            int idAccount = 0;
            if (!AccountsEmpty())
            {
                idAccount = Accounts.Max(t => t.AccountId);
            }
            idAccount++;
            Accounts.Add(new Account(idAccount, accountName));
            AccountDataBase.UpdateAccounts(Accounts, idPerson);
        }
    }
}
