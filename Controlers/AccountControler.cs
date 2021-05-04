using PersonalFinanceControlConsole.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Controlers
{
    class AccountControler
    {
        public Account Account;

        public AccountControler()
        {
        }

        public AccountControler(Account account)
        {
            Account = account;
        }

        internal string[,] GetAccountInfo()
        {
            return Account.GetInfos();
        }

        internal string GetAccountName()
        {
            return Account.AccountName;
        }

    }
}
