using PersonalFinanceControlConsole.Controlers;
using PersonalFinanceControlConsole.Menus.Structs;
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
                case MenuStructure.Back: break;
                default: ManageAccounts(); break;
            }
        }
        
        private void AddAccount()
        {
            var sAccount = Menus.MenuOptions.AddAccountScreen();
            if (sAccount.OptionBack)
                ManageAccounts();
            else
                InsertAccount(sAccount);
        }

        private void InsertAccount(SAccount sAccount)
        {
            if (accountListControler.VerifyExistingAccount(sAccount.Name))
            {
                MenuDefaults.Message("You already have an account " + sAccount.Name);
                AddAccount();
                return;
            }
            accountListControler.InsertAccount(personControler.GetId(), sAccount);
            ManageAccounts();
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
                case MenuStructure.Back: ManageAccounts(); break;
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

    }
}
