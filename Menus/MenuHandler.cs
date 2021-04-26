using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using PersonalFinanceControlConsole.Controlers;

namespace PersonalFinanceControlConsole.Menus
{
    
    class MenuHandler
    {
        public PersonControler personControler = new PersonControler();
        public AccountControler accountControler = new AccountControler();
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
            int userId = 0;
            while (userId == 0)
            {
                userId = Menus.MenuOptions.LoginScreen();
            }
            return userId;
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
            accountControler.Accounts = personControler.GetAccounts();
            string userName = personControler.GetUserName();
            MenuOptions.WellcomeScreen(userName);
            string optionStr = MenuDefault.ReadLine(() => Wellcome(), Menus.Enums.ETypeRead.Int);
            var option = int.Parse(optionStr);
            switch (option)
            {
                case 1: ManageProfile(userName); break;
                case 2: ManageAccounts(); break;
                default: Wellcome(); break;
            }
        }
        private void ManageProfile(string userName)
        {
            MenuOptions.ManageProfileScreen(userName);
            string option = MenuDefault.ReadLine(() => ManageProfile(userName), Menus.Enums.ETypeRead.String);
            switch (option)
            {
                case "9": personControler.DeleteUser(); break;
                case "*": Wellcome(); break;
                default: ManageProfile(userName); break;
            }
        }

        private void ManageAccounts()
        {
            if (accountControler.AccountsEmpty())
            {
                AddAccount();
            }
            string userName = personControler.GetUserName();
            MenuOptions.ManageAccountsScreen(userName);
            string option = MenuDefault.ReadLine(() => Wellcome(), Menus.Enums.ETypeRead.String);
            switch (option)
            {
                case "1": AddAccount(); break;
                case "*": Wellcome(); break;
                default: ManageAccounts(); break;
            }
        }

        private void AddAccount()
        {
            MenuOptions.AddAccountScreen();
            string accountName = MenuDefault.ReadLine(() => AddAccount(), Menus.Enums.ETypeRead.String);
            switch (accountName)
            {
                case "*": ManageAccounts(); break;
                default: InsertAccount(accountName); break;
            }
        }
        private void InsertAccount(string accountName)
        {
            if (accountControler.VerifyExistingAccount(accountName))
            {
                MenuDefault.Message("You already have an account " + accountName);
                AddAccount();
                return;
            }
            accountControler.InsertAccount(personControler.GetId(), accountName);
            ManageAccounts();
        }
    }
}
