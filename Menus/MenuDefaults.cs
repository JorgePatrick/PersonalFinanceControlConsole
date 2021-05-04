using PersonalFinanceControlConsole.Menus.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Menus
{
    static class MenuDefaults
    {
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
            MenuStructure.AddLine(1);
            MenuStructure.WriteLine(listType + ": ");
            return MenuStructure.ReadLine(method, ETypeRead.NumberList, optionIndex);
        }

        internal static bool AreYouSureScreen(string message, Action method)
        {
            MenuStructure.DrawScreen();
            MenuStructure.SetTitle(message);
            MenuStructure.AddLine(1);
            MenuStructure.WriteLine("Are you sure? (Y/N) ");
            var anwser = MenuStructure.ReadLine(method, ETypeRead.YesOrNo);
            return anwser == "Y";
        }

        internal static void Message(string message)
        {
            MenuStructure.DrawScreen();
            MenuStructure.SetTitle("Message");
            MenuStructure.AddLine(1);
            MenuStructure.WriteNewLine(message);
            MenuStructure.ReadKey();
        }


    }
}
