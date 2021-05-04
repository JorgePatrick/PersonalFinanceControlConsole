using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using PersonalFinanceControlConsole.Controlers;
using PersonalFinanceControlConsole.Menus.Enums;

namespace PersonalFinanceControlConsole.Menus
{

    class MenuHandler
    {
        public PersonControler personControler = new PersonControler();
        public AccountListControler accountListControler = new AccountListControler();
        public AccountControler accountControler;
        int userId;

        public void Show()
        {
            userId = Login();

            if (personControler.UserNotExists(userId))
            {
                Register();
            }

            Wellcome();
        }

        private int Login()
        {
            int idNumber = 0;
            while (idNumber == 0)
            {
                idNumber = Menus.MenuOptions.LoginScreen();
            }
            return idNumber;
        }

        private void Register()
        {
            string userName = "invalid";
            while (userName == "invalid")
            {
                userName = Menus.MenuOptions.RegisterScreen(userId);
            }
            personControler.InsertUser(userId, userName);
        }

        private void Wellcome()
        {
            accountListControler.SetAccounts(personControler.GetAccounts());

            var title = "Wellcome " + personControler.GetUserName();
            string[] optionList =
               {
                "Manage Profile",
                "Manage Accounts"
               };
            string option = MenuDefaults.ListScreen(title, "Option", optionList, () => Wellcome());
            switch (option)
            {
                case "1": ManageProfile(); break;
                case "2": ManageAccounts(); break;
                case "*": Show(); break;
                default: Wellcome(); break;
            }
        }

        private void ManageProfile()
        {
            var title = "Manage Profile " + personControler.GetUserName();
            string[] optionList =
               {
                "Change Name",
                "Delete User"
               };
            string option = MenuDefaults.ListScreen(title, "Option", optionList, () => ManageProfile());
            switch (option)
            {
                case "1": ChangeName(); break;
                case "2": DeleteUser(); break;
                case "*": Wellcome(); break;
                default: ManageProfile(); break;
            }
        }

        private void ChangeName()
        {
            string newName = "invalid";
            while (newName == "invalid")
            {
                newName = Menus.MenuOptions.ChangeName(personControler.GetUserName());
            }
            switch (newName)
            {
                case "*": ManageProfile(); break;
                default: UpdateName(newName); break;
            }
        }

        private void DeleteUser()
        {
            if (MenuDefaults.AreYouSureScreen("Delete User", () => DeleteUser()))
            {
                personControler.DeleteUser();
                Show();
            }
            else
            {
                ManageProfile();
            }
        }

        private void ManageAccounts()
        {
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
                case "*": Wellcome(); break;
                default: ManageAccounts(); break;
            }
        }

        private void ListAccounts()
        {
            var title = "Accounts List " + personControler.GetUserName();
            string[] AccountList = accountListControler.GetList();
            string account = MenuDefaults.ListScreen(title, "Option", AccountList, () => ListAccounts());
            switch (account)
            {
                case "*": ManageAccounts(); break;
                default: AccountTransactions(int.Parse(account)); break;
            }
        }

        private void AccountTransactions(int account)
        {
            var title = "Account Transactions " + accountControler.GetAccountName(); 
            string[] optionList =
               {
                "Delete Account",
                "Account Infos"
               };
            string option = MenuDefaults.ListScreen(title, "Option", optionList, () => ManageProfile());
            switch (option)
            {
                case "1": DeleteAccount(account); break;
                case "2": ShowAccount(account); break;
                case "*": Wellcome(); break;
                default: ManageProfile(); break;
            }
        }

        private void DeleteAccount(int account)
        {
            if (MenuDefaults.AreYouSureScreen("Delete Account", () => DeleteAccount(account)))
            {
                accountListControler.DeleteAccount(personControler.GetId(), account);
                if (accountListControler.AccountsEmpty())
                    Wellcome();
                else
                    ListAccounts();
            }
            else
            {
                AccountTransactions(account);
            }
        }

        private void ShowAccount(int account)
        {
            string[,] accounInfo = accountControler.GetAccountInfo();
            MenuOptions.ShowAccount(accounInfo);
            AccountTransactions(account);
        }

        private void UpdateName(string newName)
        {
            if (MenuDefaults.AreYouSureScreen("Change Name", () => UpdateName(newName)))
            {
                personControler.UpdateName(newName);
            }
            ManageProfile();
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
