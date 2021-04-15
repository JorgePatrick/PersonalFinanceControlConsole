using System;
using System.Linq;
using MongoDB.Driver;

namespace PersonalFinanceControlConsole
{
    class Menu
    {
        static IMongoCollection<Person> Collection;

        public static void Login(IMongoCollection<Person> collection)
        {
            MenuDefault.DrawScreen();
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
            int currentLine = MenuDefault.SetTitle("Login");
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
            MenuDefault.DrawScreen();
            int currentLine = MenuDefault.SetTitle("Register");
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
            MenuDefault.DrawScreen();
            int currentLine = MenuDefault.SetTitle("Wellcome " + user.Name);
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
            MenuDefault.DrawScreen();
            MenuDefault.SetGoBackOption();
            int currentLine = MenuDefault.SetTitle("Add Account");
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
        private static void CheckExit(int option)
        {
            if (option == 0)
            {
                Console.Clear();
                System.Environment.Exit(0);
            }
        }
        private static void Message(string message)
        {
            MenuDefault.DrawScreen();
            int currentLine = MenuDefault.SetTitle("Message");
            currentLine++;
            currentLine = WriteNewLine(currentLine, message);
            Console.ReadKey();
        }

    }
}
