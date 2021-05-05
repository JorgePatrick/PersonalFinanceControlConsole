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
        private readonly PersonControler personControler = new PersonControler();
        int UserId;

        public void Show()
        {
            UserId = Login();

            if (personControler.UserNotExists(UserId))
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
                userName = Menus.MenuOptions.RegisterScreen(UserId);
            }
            personControler.InsertUser(UserId, userName);
        }

        private void Wellcome()
        {
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
                case MenuStructure.Back: Show(); break;
                default: Wellcome(); break;
            }
        }

        private void ManageProfile()
        {
            var menuProfile = new MenuProfile(personControler);
            menuProfile.ManageProfile();
            if (personControler.UserNotExists(UserId))
                Show();
            else
                Wellcome();
        }

        private void ManageAccounts()
        {
            var menuAccounts = new MenuAccountList(personControler);
            menuAccounts.ManageAccounts();
            Wellcome();
        }
    }
}
