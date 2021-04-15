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
            Collection = collection;
            Person user = MenuLoginRegister.Login();
            WellcomeScreen(user);
        }

        internal static Person ReadUser(int userId)
        {
            var query =
                from e in Collection.AsQueryable<Person>()
                where e.UserId == userId
                select e;
            return query.FirstOrDefault();
        }

        internal static void Save(Person user)
        {
            user.PeopleCollection = Collection;
            Collection.InsertOne(user);
        }

        private static void WellcomeScreen(Person user)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Wellcome " + user.Name);
            MenuDefault.WriteNewLine("Choose one option:");
            MenuDefault.WriteNewLine("1 - Add Account");
            MenuDefault.WriteNewLine("9 - Delete User");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Option: ");
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
            MenuDefault.SetTitle("Add Account");
            MenuDefault.WriteNewLine("Fill the info above");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Account Name: ");
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

        internal static void CheckExit(int option)
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
            MenuDefault.SetTitle("Message");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteNewLine(message);
            Console.ReadKey();
        }

    }
}
