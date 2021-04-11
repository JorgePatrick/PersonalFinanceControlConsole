using MongoDB.Driver;
using System;

namespace PersonalFinanceControlConsole
{
    class Menu
    {
        static int ScreenSizeLines = 15;
        static int ScreenSizeCols = 34;
        static IMongoDatabase Database;

        public static void Login(IMongoDatabase database)
        {
            Database = database;
            DrawScreen();
            int idLogin = WriteLogin();
            var user = new Person(idLogin, Database);
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
            int currentLine = SetTitle("Register");
            currentLine = WriteLine(currentLine, "Id: " + user.IdLogin);
            currentLine = WriteLine(currentLine, "Enter your name: ");
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
            int currentLine = SetTitle("Wellcome " + user.Name);
            currentLine = WriteNewLine(currentLine, "Choose one option:");
            currentLine = WriteNewLine(currentLine, "1 - Add Account");
            currentLine = WriteNewLine(currentLine, "9 - Delete User");
            currentLine++;
            currentLine = WriteLine(currentLine, "Opção: ");
            var option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1: AddAccount(user); break;
                case 9: user.Delete(); Login(Database); break;
                case 0: CheckExit(option); break;
                default: WellcomeScreen(user); break;
            }
        }
        private static void AddAccount(Person user)
        {
            DrawScreen();
            int currentLine = SetTitle("Add Account");
            currentLine = WriteNewLine(currentLine, "Fill the info above");
            currentLine++;
            currentLine = WriteLine(currentLine, "Account Name: ");
            var accountName = Console.ReadLine();
        }
        private static int WriteLogin()
        {
            int currentLine = SetTitle("Login");
            currentLine = WriteLine(currentLine, "Type your Id: ");
            var idLogin = int.Parse(Console.ReadLine());
            CheckExit(idLogin);
            return idLogin;
        }
        private static int SetTitle(string title)
        {
            Console.SetCursorPosition(3, 4);
            Console.Write(title);
            Console.SetCursorPosition(3, 5);
            for (int i = 0; i <= (ScreenSizeCols - 4); i++)
            {
                Console.Write("-");
            }
            return 6;
        }
        private static int WriteNewLine(int line, string text)
        {
            line++;
            Console.SetCursorPosition(3, line);
            Console.WriteLine(text);
            return line;
        }
        private static int WriteLine(int line, string text)
        {
            line++;
            Console.SetCursorPosition(3, line);
            Console.Write(text);
            return line;
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
            for (int i = 0; i <= ScreenSizeCols; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            Console.Write("\n");
        }
        private static void DrawMiddleLine()
        {
            Console.Write("|");
            for (int i = 0; i <= ScreenSizeCols; i++)
            {
                Console.Write(" ");
            }
            Console.Write("|");
            Console.Write("\n");
        }
        private static void Header()
        {
            Console.SetCursorPosition((ScreenSizeCols / 2 - 6), 2);
            Console.WriteLine("Finance Control");
            Console.SetCursorPosition(3, 3);
            for (int i = 0; i <= (ScreenSizeCols - 4); i++)
            {
                Console.Write("=");
            }
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
