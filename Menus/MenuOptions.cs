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
            MenuStructure.DrawScreen();
            MenuStructure.SetTitle("Login");
            MenuStructure.WriteLine("Type your Id: ");
            string userIdStr = MenuStructure.ReadLine(null, ETypeRead.Int);
            if (userIdStr == "invalid")
            {
                return 0;
            }
            return int.Parse(userIdStr);
        }
        public static string RegisterScreen(int userId)
        {
            MenuStructure.DrawScreen();
            MenuStructure.SetTitle("Register");
            MenuStructure.WriteLine("Id: " + userId);
            MenuStructure.WriteLine("Enter your name: ");
            return MenuStructure.ReadLine(null, ETypeRead.String);
        }

        internal static string ListScreen(string title, string listType, string[] optionList, Action method)
        {
            int optionIndex = 0;

            MenuStructure.DrawScreen();
            MenuStructure.SetGoBackOption();
            MenuStructure.SetTitle(title);
            MenuStructure.WriteNewLine("Choose one " + listType + ":");
            foreach (var optionText in optionList)
            {
                optionIndex++;
                MenuStructure.WriteNewLine(optionIndex + " - " + optionText);
            }
            MenuStructure.CurrentLine++;
            MenuStructure.WriteLine(listType + ": ");
            return MenuStructure.ReadLine(method, ETypeRead.NumberList, optionIndex);
        }

        internal static string ChangeName(string userName)
        {
            MenuStructure.DrawScreen();
            MenuStructure.SetGoBackOption();
            MenuStructure.SetTitle("Change Name");
            MenuStructure.WriteLine("Name: " + userName);
            MenuStructure.WriteLine("Enter new name: ");
            return MenuStructure.ReadLine(null, ETypeRead.String);
        }

        internal static string AreYouSureScreen(string message, Action method)
        {
            MenuStructure.DrawScreen();
            MenuStructure.SetTitle(message);
            MenuStructure.CurrentLine++;
            MenuStructure.WriteLine("Are you sure? (Y/N) ");
            return MenuStructure.ReadLine(method, ETypeRead.YesOrNo);
        }

        internal static void Message(string message)
        {
            MenuStructure.DrawScreen();
            MenuStructure.SetTitle("Message");
            MenuStructure.CurrentLine++;
            MenuStructure.WriteNewLine(message);
            MenuStructure.ReadKey();
        }

        internal static string AddAccountScreen()
        {
            MenuStructure.DrawScreen();
            MenuStructure.SetGoBackOption();
            MenuStructure.SetTitle("Add Account");
            MenuStructure.WriteNewLine("Fill the info above");
            MenuStructure.CurrentLine++;
            MenuStructure.WriteLine("Account Name: ");
            return MenuStructure.ReadLine(null, ETypeRead.String);
        }

        internal static void ShowAccount(string[,] accounInfo)
        {
            MenuStructure.DrawScreen();
            MenuStructure.SetTitle("Account Infos");
            for (int i = 0; i < accounInfo.GetLength(0); i++)
            {
                MenuStructure.WriteNewLine(accounInfo[i, 0] + ": " + accounInfo[i, 1]);
            }
            MenuStructure.ReadKey();
        }
    }
}
