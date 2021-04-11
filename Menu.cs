using MongoDB.Driver;
using System;

namespace PersonalFinanceControlConsole
{
    class Menu
    {
        public static void Login(IMongoDatabase database)
        {
            DrawScreen();
            int idLogin = WriteLogin();
            var user = new Person(idLogin, database);
            if (user.Name != "")
            {
                WellcomeScreen(user.Name);
            }
        }
        private static void WellcomeScreen(string name)
        {
            DrawScreen();
            Header();
            Footnote();
            Console.SetCursorPosition(3, 4);
            Console.Write("Wellcome " + name + ": ");
            var option = int.Parse(Console.ReadLine());
            CheckExit(option);
        }

        public static void DrawScreen()
        {
            Console.Clear();
            DrawFirstLastLine();
            for (int lines = 0; lines <= 10; lines++)
            {
                DrawMiddleLine();
            }
            DrawFirstLastLine();
        }
        private static void DrawFirstLastLine()
        {
            Console.Write("+");
            for (int i = 0; i <= 30; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            Console.Write("\n");
        }
        private static void DrawMiddleLine()
        {
            Console.Write("|");
            for (int i = 0; i <= 30; i++)
            {
                Console.Write(" ");
            }
            Console.Write("|");
            Console.Write("\n");
        }
        private static int WriteLogin()
        {
            Header();
            Footnote();
            Console.SetCursorPosition(3, 4);
            Console.Write("Type your Id: ");
            var idLogin = int.Parse(Console.ReadLine());
            CheckExit(idLogin);
            return idLogin;
        }

        private static void Header()
        {
            Console.SetCursorPosition(9, 2);
            Console.WriteLine("Finance Control");
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("===========================");
        }
        private static void Footnote()
        {
            Console.SetCursorPosition(3, 10);
            Console.WriteLine("Type 0 to Exit");
        }
        private static void CheckExit(int option)
        {
            if (option == 0)
            {
                Console.Clear();
                System.Environment.Exit(0);
            }
        }
    }
}
