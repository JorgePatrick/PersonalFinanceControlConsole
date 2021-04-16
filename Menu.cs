using System;
using System.Linq;
using MongoDB.Driver;
using PersonalFinanceControlConsole.Menus;

namespace PersonalFinanceControlConsole
{
    class Menu
    {
        static IMongoCollection<Person> Collection;

        public static void Login(IMongoCollection<Person> collection)
        {
            Collection = collection;
            int userId = 0;
            while (userId == 0)
            {
                userId = Menus.MenuOptions.WriteLogin();
            }

            Person user = Menu.ReadUser(userId);
            while (user.NotExists())
            {
                Menus.MenuOptions.Register(user);
            }

            var option = WellcomeScreen(user);
            switch (option)
            {
                case 1: AddAccount(user); break;
                case 9: DeleteUser(user); break;
                default: WellcomeScreen(user); break;
            }
        }

        private static void DeleteUser(Person user)
        {
            var filter = Builders<Person>.Filter.Eq("_id", user.Id);
            Collection.DeleteOne(filter);
            Login(Collection);
        }

        internal static Person ReadUser(int userId)
        {
            var query =
                from e in Collection.AsQueryable<Person>()
                where e.UserId == userId
                select e;
            var user = query.FirstOrDefault();
            if (user == null)
            {
                user = new Person();
                user.UserId = userId;
            }
            return user;
        }

        internal static void Save(Person user)
        {
            user.PeopleCollection = Collection;
            Collection.InsertOne(user);
        }

        private static int WellcomeScreen(Person user)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Wellcome " + user.Name);
            MenuDefault.WriteNewLine("Choose one option:");
            MenuDefault.WriteNewLine("1 - Add Account");
            MenuDefault.WriteNewLine("9 - Delete User");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Option: ");
            string optionStr = MenuDefault.ReadLine(() => WellcomeScreen(user), Menus.Enums.ETypeRead.Int);
            return int.Parse(optionStr);
        }
        private static void AddAccount(Person user)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetGoBackOption();
            MenuDefault.SetTitle("Add Account");
            MenuDefault.WriteNewLine("Fill the info above");
            MenuDefault.CurrentLine++;
            MenuDefault.WriteLine("Account Name: ");
            string accountName = MenuDefault.ReadLine(() => AddAccount(user), Menus.Enums.ETypeRead.String);
            switch (accountName)
            {
                case "9": WellcomeScreen(user); break;
                case "0": MenuDefault.CheckExit("0"); break;
                default:
                    {
                        if (user.VerifyExistingAccount(accountName))
                        {
                            MenuDefault.Message("You already have an account " + accountName);
                            AddAccount(user);
                        }
                    }; break;
            }
        }
    }
}
