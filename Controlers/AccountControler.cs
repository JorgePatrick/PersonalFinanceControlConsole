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
        private List<Account> Accounts;

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

        internal string[] GetList()
        {
            string[] accountsList = new string[Accounts.Count];
            var accountIndex = 0;
            foreach (var account in Accounts)
            {
                accountsList[accountIndex] = account.AccountName;
                accountIndex++;
            }
            return accountsList;
        }

        internal void SetAccounts(List<Account> accountList)
        {
            Accounts = accountList;
        }

        internal string[,] GetAccountInfo(int accountId)
        {
            var account = Accounts.FirstOrDefault(x => x.AccountId == accountId);
            return account.GetInfos();
        }

        internal string GetAccountName(int accountId)
        {
            var account = Accounts.FirstOrDefault(x => x.AccountId == accountId);
            return account.AccountName;
        }
    }
}
