using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Menus
{
    class MenuOptions
    {
        public static int LoginScreen()
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Login");
            MenuDefault.WriteLine("Type your Id: ");
            string userIdStr = MenuDefault.ReadLine(null, Menus.Enums.ETypeRead.Int);
            if (userIdStr == "invalid")
            {
                return 0;
            }
            return int.Parse(userIdStr);
        }
        public static string RegisterScreen(int userId)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Register");
            MenuDefault.WriteLine("Id: " + userId);
            MenuDefault.WriteLine("Enter your name: ");
            return MenuDefault.ReadLine(null, Menus.Enums.ETypeRead.String);
        }

        internal static void WellcomeScreen(string name)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Wellcome " + name);
            MenuDefault.WriteNewLine("Choose one option:");
            MenuDefault.WriteNewLine("1 - Manage Profile");
            MenuDefault.WriteNewLine("2 - Manage Accounts");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Option: ");
        }

        internal static void ManageProfileScreen(string name)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetGoBackOption();
            MenuDefault.SetTitle("Manage Profile " + name);
            MenuDefault.WriteNewLine("Choose one option:");
            MenuDefault.WriteNewLine("9 - Delete User");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Option: ");
        }

        internal static void ManageAccountsScreen(string name)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetGoBackOption();
            MenuDefault.SetTitle("Manage Accounts " + name);
            MenuDefault.WriteNewLine("Choose one option:");
            MenuDefault.WriteNewLine("1 - Add Account");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Option: ");
        }

        internal static void AddAccountScreen()
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetGoBackOption();
            MenuDefault.SetTitle("Add Account");
            MenuDefault.WriteNewLine("Fill the info above");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Account Name: ");
        }
    }
}
