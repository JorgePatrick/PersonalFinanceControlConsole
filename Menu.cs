using System;
using System.Linq;
using MongoDB.Driver;

namespace PersonalFinanceControlConsole
{
    class Menu
    {
        static int ScreenSizeLines = 15;
        static int ScreenSizeCols = 34;
        static IMongoCollection<Person> Collection;

        public static void Login(IMongoCollection<Person> collection)
        {
            DrawScreen();
            Collection = collection;
            int userId = WriteLogin();
            Person user = ReadUser(userId);
            if (user == null)
            {
                user = new Person();
                string userName = Register(userId);
                Save(user, userId, userName);
            }
            WellcomeScreen(user);
        }

        private static Person ReadUser(int userId)
        {
            var query =
                from e in Collection.AsQueryable<Person>()
                where e.UserId == userId
                select e;
            return query.FirstOrDefault();
        }

        private static void Save(Person user, int userId, string userName)
        {
            user.UserId = userId;
            user.Name = userName;
            user.PeopleCollection = Collection;
            Collection.InsertOne(user);
        }

        private static int WriteLogin()
        {
            int currentLine = SetTitle("Login");
            currentLine = WriteLine(currentLine, "Type your Id: ");
            int userId;
            bool userIdIsNumber = int.TryParse(Console.ReadLine(), out userId);
            if (!userIdIsNumber)
            {
                WriteLogin();
            }
            CheckExit(userId);
            return userId;
        }
        private static string Register(int userId)
        {
            DrawScreen();
            int currentLine = SetTitle("Register");
            currentLine = WriteLine(currentLine, "Id: " + userId);
            currentLine = WriteLine(currentLine, "Enter your name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Register(userId);
            }
            if (name == "0")
            {
                CheckExit(0);
            }
            return name;
        }
        private static void WellcomeScreen(Person user)
        {
            DrawScreen();
            int currentLine = SetTitle("Wellcome " + user.Name);
            currentLine = WriteNewLine(currentLine, "Choose one option:");
            currentLine = WriteNewLine(currentLine, "1 - Add Account");
            currentLine = WriteNewLine(currentLine, "9 - Delete User");
            currentLine++;
            currentLine = WriteLine(currentLine, "Option: ");
            int option;
            bool optionIsNumber = int.TryParse(Console.ReadLine(), out option);
            if (!optionIsNumber)
            {
                WellcomeScreen(user);
            }
            switch (option)
            {
                case 1: AddAccount(user); break;
                case 9:
                    {
                        var filter = Builders<Person>.Filter.Eq("_id", user.Id);
                        Collection.DeleteOne(filter);
                        Login(Collection); break;
                    }
                case 0: CheckExit(option); break;
                default: WellcomeScreen(user); break;
            }
        }
        private static void AddAccount(Person user)
        {
            DrawScreen();
            Console.SetCursorPosition(18, ScreenSizeLines);
            Console.Write("/ 9 to back");
            int currentLine = SetTitle("Add Account");
            currentLine = WriteNewLine(currentLine, "Fill the info above");
            currentLine++;
            currentLine = WriteLine(currentLine, "Account Name: ");
            var accountName = Console.ReadLine();
            if (string.IsNullOrEmpty(accountName))
            {
                AddAccount(user);
            }
            switch (accountName)
            {
                case "9": WellcomeScreen(user); break;
                case "0": CheckExit(0); break;
                default:
                    {
                        if (user.VerifyExistingAccount(accountName))
                        {
                            Message("You already have an account " + accountName);
                            AddAccount(user);
                        }
                    }; break;
            }
        }
        private static void Message(string message)
        {
            DrawScreen();
            int currentLine = SetTitle("Message");
            currentLine++;
            currentLine = WriteNewLine(currentLine, message);
            Console.ReadKey();
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
