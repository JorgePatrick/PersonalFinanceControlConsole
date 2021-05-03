using PersonalFinanceControlConsole.Menus.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Menus
{
    static class MenuOptions
    {
        public static int LoginScreen()
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Login");
            MenuDefault.WriteLine("Type your Id: ");
            string userIdStr = MenuDefault.ReadLine(null, ETypeRead.Int);
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
            return MenuDefault.ReadLine(null, ETypeRead.String);
        }

        internal static string ListScreen(string title, string listType, string[] optionList, Action method)
        {
            int optionIndex = 0;

            MenuDefault.DrawScreen();
            MenuDefault.SetGoBackOption();
            MenuDefault.SetTitle(title);
            MenuDefault.WriteNewLine("Choose one " + listType + ":");
            foreach (var optionText in optionList)
            {
                optionIndex++;
                MenuDefault.WriteNewLine(optionIndex + " - " + optionText);
            }
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine(listType + ": ");
            return MenuDefault.ReadLine(method, ETypeRead.NumberList, optionIndex);
        }

        internal static string ChangeName(string userName)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetGoBackOption();
            MenuDefault.SetTitle("Change Name");
            MenuDefault.WriteLine("Name: " + userName);
            MenuDefault.WriteLine("Enter new name: ");
            return MenuDefault.ReadLine(null, ETypeRead.String);
        }

        internal static string AreYouSureScreen(string message, Action method)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle(message);
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Are you sure? (Y/N) ");
            return MenuDefault.ReadLine(method, ETypeRead.YesOrNo);
        }

        internal static void Message(string message)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Message");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteNewLine(message);
            MenuDefault.ReadKey();
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

        internal static void ShowAccount(string[,] accounInfo)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Account Infos");
            for (int i = 0; i < accounInfo.GetLength(0); i++)
            {
                MenuDefault.WriteNewLine(accounInfo[i, 0] + ": " + accounInfo[i, 1]);
            }
            MenuDefault.ReadKey();
        }
    }
}
