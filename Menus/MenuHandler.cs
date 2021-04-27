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
            MenuOptions.WellcomeScreen(personControler.GetUserName());
            string option = MenuDefault.ReadLine(() => Wellcome(), ETypeRead.String);
            switch (option)
            {
                case "1": ManageProfile(); break;
                case "2": ManageAccounts(); break;
                default: Wellcome(); break;
            }
        }
        private void ManageProfile()
        {
            MenuOptions.ManageProfileScreen(personControler.GetUserName());
            string option = MenuDefault.ReadLine(() => ManageProfile(), ETypeRead.String);
            switch (option)
            {
                case "1": ChangeName(); break;
                case "9": DeleteUser(); break;
                case "*": Wellcome(); break;
                default: ManageProfile(); break;
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
            string option = MenuDefault.ReadLine(() => ManageAccounts(), ETypeRead.String);
            switch (option)
            {
                case "1": AddAccount(); break;
                case "*": Wellcome(); break;
                default: ManageAccounts(); break;
            }
        }

        private void DeleteUser()
        {
            string anwser = Menus.MenuOptions.AreYouSureScreen("Delete User", () => DeleteUser());
            if (anwser == "Y")
            {
                personControler.DeleteUser();
                Show();
            }
            else
                ManageProfile();
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

        private void UpdateName(string newName)
        {
            string anwser = Menus.MenuOptions.AreYouSureScreen("Change Name", () => UpdateName(newName));
            if (anwser == "Y")
            {
                personControler.UpdateName(newName);
                ManageProfile();
            }
            else
                ManageProfile();
        }

        private void AddAccount()
        {
            MenuOptions.AddAccountScreen();
            string accountName = MenuDefault.ReadLine(() => AddAccount(), ETypeRead.String);
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
                MenuOptions.Message("You already have an account " + accountName);
                AddAccount();
                return;
            }
            accountControler.InsertAccount(personControler.GetId(), accountName);
            ManageAccounts();
        }
    }
}
