using PersonalFinanceControlConsole.Controlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Menus
{
    class MenuAccountList
    {
        private readonly PersonControler personControler;
        private readonly AccountListControler accountListControler = new AccountListControler();

        public MenuAccountList(PersonControler personControler)
        {
            this.personControler = personControler;
        }

        internal void ManageAccounts()
        {
            accountListControler.SetAccounts(personControler.GetAccounts());
            if (accountListControler.AccountsEmpty())
            {
                AddAccount();
            }

            var title = "Manage Accounts " + personControler.GetUserName();
            string[] optionList =
               {
                "Add Account",
                "List Accounts"
               };
            string option = MenuDefaults.ListScreen(title, "Option", optionList, () => ManageAccounts());
            switch (option)
            {
                case "1": AddAccount(); break;
                case "2": ListAccounts(); break;
                case "*": break;
                default: ManageAccounts(); break;
            }
        }
        private void ListAccounts()
        {
            if (accountListControler.AccountsEmpty())
                ManageAccounts();

            var title = "Accounts List " + personControler.GetUserName();
            string[] AccountList = accountListControler.GetList();
            string account = MenuDefaults.ListScreen(title, "Option", AccountList, () => ListAccounts());
            switch (account)
            {
                case "*": ManageAccounts(); break;
                default: ManageSingleAccount(account); break;
            }
        }

        private void ManageSingleAccount(string account)
        {
            var accountId = int.Parse(account);
            var menuAccount = new MenuAccount(personControler, accountListControler, accountId);
            menuAccount.AccountTransactions();
            ListAccounts();
        }

        private void AddAccount()
        {
            string accountName = "invalid";
            while (accountName == "invalid")
            {
                accountName = Menus.MenuOptions.AddAccountScreen();
            }
            switch (accountName)
            {
                case "*": ManageAccounts(); break;
                default: InsertAccount(accountName); break;
            }
        }

        private void InsertAccount(string accountName)
        {
            if (accountListControler.VerifyExistingAccount(accountName))
            {
                MenuDefaults.Message("You already have an account " + accountName);
                AddAccount();
                return;
            }
            accountListControler.InsertAccount(personControler.GetId(), accountName);
            ManageAccounts();
        }

    }
}
