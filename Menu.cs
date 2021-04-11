using MongoDB.Driver;
using System;

namespace PersonalFinanceControlConsole
{
    class Menu
    {
        static int ScreenSizeLines = 15;
        static IMongoDatabase Database;

        public static void Login(IMongoDatabase database)
        {
            Database = database;
            DrawScreen();
            int idLogin = WriteLogin();
            var user = new Person(idLogin, database);
            if (string.IsNullOrEmpty(user.Name))
            {
                Register(user);
                user.Save();
            }
            WellcomeScreen(user);
        }

        private static void Register(Person user)
        {
            DrawScreen();
            int initialLine = SetTitle("Register");
            Console.SetCursorPosition(3, initialLine);
            Console.Write("Id: " + user.IdLogin);
            initialLine++;
            Console.SetCursorPosition(3, initialLine);
            Console.Write("Enter your name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Register(user);
            }
            if (name == "0")
            {
                CheckExit(0);
            }
            user.Name = name;
        }

        private static void WellcomeScreen(Person user)
        {
            DrawScreen();
            int initialLine = SetTitle("Wellcome " + user.Name);
            Console.SetCursorPosition(3, initialLine);
            Console.WriteLine("Choose one option:");
            initialLine++;
            Console.SetCursorPosition(3, initialLine);
            Console.WriteLine("9 - Delete User");
            initialLine++;
            initialLine++;
            Console.SetCursorPosition(3, initialLine);
            Console.Write("Opção: ");
            var option = int.Parse(Console.ReadLine());
            CheckExit(option);
            if (option == 9)
            {
                user.Delete();
                Login(Database);
            }

        }
        private static int WriteLogin()
        {
            int initialLine = SetTitle("Login");
            Console.SetCursorPosition(3, initialLine);
            Console.Write("Type your Id: ");
            var idLogin = int.Parse(Console.ReadLine());
            CheckExit(idLogin);
            return idLogin;
        }

        private static int SetTitle(string title)
        {
            Console.SetCursorPosition(3, 4);
            Console.Write(title);
            Console.SetCursorPosition(3, 5);
            Console.WriteLine("---------------------------");
            return 7;
        }

        public static void DrawScreen()
        {
            Console.Clear();
            DrawFirstLastLine();
            for (int lines = 0; lines <= ScreenSizeLines; lines++)
            {
                DrawMiddleLine();
            }
            DrawFirstLastLine();
            Header();
            Footnote();
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
        private static void Header()
        {
            Console.SetCursorPosition(9, 2);
            Console.WriteLine("Finance Control");
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("===========================");
        }
        private static void Footnote()
        {
            Console.SetCursorPosition(3, ScreenSizeLines);
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
