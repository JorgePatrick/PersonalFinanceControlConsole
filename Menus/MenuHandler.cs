using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using PersonalFinanceControlConsole.Menus;

namespace PersonalFinanceControlConsole
{
    class MenuHandler
    {
        public static void Show()
        {
            int userId = Login();

            if (Controler.UserNotExists(userId))
            {
                Register(userId);
            }

            Wellcome(userId);
        }

        private static void Register(int userId)
        {
            string userName = "invalid";
            while (userName == "invalid")
            {
                userName = Menus.MenuOptions.RegisterScreen(userId);
            }
            Controler.InsertUser(userId, userName);
        }

        private static int Login()
        {
            int userId = 0;
            while (userId == 0)
            {
                userId = Menus.MenuOptions.LoginScreen();
            }
            return userId;
        }

        private static void Wellcome(int userId)
        {
            string userName = Controler.GetUserName(userId);
            MenuOptions.WellcomeScreen(userName);
            string optionStr = MenuDefault.ReadLine(() => Wellcome(userId), Menus.Enums.ETypeRead.Int);
            var option = int.Parse(optionStr);
            switch (option)
            {
                case 1: ManageProfile(userId, userName); break;
                case 2: ManageAccounts(userId); break;
                default: Wellcome(userId); break;
            }
        }
        private static void ManageProfile(int userId, string userName)
        {
            MenuOptions.ManageProfileScreen(userName);
            string option = MenuDefault.ReadLine(() => ManageProfile(userId, userName), Menus.Enums.ETypeRead.Int);
            switch (option)
            {
                case "9": Controler.DeleteUser(userId); break;
                case "*": Wellcome(userId); break;
                default: ManageProfile(userId, userName); break;
            }
        }

        private static void ManageAccounts(int userId)
        {
            if (Controler.AccountsEmpty(userId))
            {
                AddAccount(userId);
            }
            string userName = Controler.GetUserName(userId);
            MenuOptions.ManageAccountsScreen(userName);
            string option = MenuDefault.ReadLine(() => Wellcome(userId), Menus.Enums.ETypeRead.Int);
            switch (option)
            {
                case "1": AddAccount(userId); break;
                case "*": Wellcome(userId); break;
                default: ManageAccounts(userId); break;
            }
        }

        private static void AddAccount(int userId)
        {
            MenuOptions.AddAccountScreen();
            string accountName = MenuDefault.ReadLine(() => AddAccount(userId), Menus.Enums.ETypeRead.String);
            switch (accountName)
            {
                case "*": ManageAccounts(userId); break;
                default: InsertAccount(userId, accountName); break;
            }
        }
        private static void InsertAccount(int userId, string accountName)
        {
            if (Controler.VerifyExistingAccount(userId, accountName))
            {
                MenuDefault.Message("You already have an account " + accountName);
                AddAccount(userId);
                return;
            }
            Controler.InsertAccount(userId, accountName);
            ManageAccounts(userId);
        }
    }
}
