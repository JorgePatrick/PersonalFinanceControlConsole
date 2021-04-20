using System;
using System.Collections.Generic;
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
                userId = Menus.MenuOptions.LoginScreen();
            }

            Person user = Menu.ReadUser(userId);
            if (user.NotExists())
            {
                string userName = "invalid";
                while (userName == "invalid")
                {
                    userName = Menus.MenuOptions.RegisterScreen(userId);
                }
                user.Name = userName;
                Menu.Save(user);
            }

            Wellcome(user);
        }

        private static void Wellcome(Person user)
        {

            MenuOptions.WellcomeScreen(user.Name);
            string optionStr = MenuDefault.ReadLine(() => Wellcome(user), Menus.Enums.ETypeRead.Int);
            var option = int.Parse(optionStr);
            switch (option)
            {
                case 1: ManageProfile(user); break;
                case 2: ManageAccounts(user); break;
                default: Wellcome(user); break;
            }
        }
        private static void ManageProfile(Person user)
        {
            MenuOptions.ManageProfileScreen(user.Name);
            string option = MenuDefault.ReadLine(() => ManageProfile(user), Menus.Enums.ETypeRead.Int);
            switch (option)
            {
                case "9": DeleteUser(user); break;
                case "*": Wellcome(user); break;
                default: ManageProfile(user); break;
            }
        }

        private static void ManageAccounts(Person user)
        {
            if (user.AccountsEmpty())
            {
                AddAccount(user);
            }
            MenuOptions.ManageAccountsScreen(user.Name);
            string option = MenuDefault.ReadLine(() => Wellcome(user), Menus.Enums.ETypeRead.Int);
            switch (option)
            {
                case "1": AddAccount(user); break;
                case "*": Wellcome(user); break;
                default: ManageAccounts(user); break;
            }
        }

        private static void AddAccount(Person user)
        {
            MenuOptions.AddAccountScreen();
            string accountName = MenuDefault.ReadLine(() => AddAccount(user), Menus.Enums.ETypeRead.String);
            switch (accountName)
            {
                case "*": ManageAccounts(user); break;
                default: InsertAccount(user, accountName); break;
            }
        }

        private static void InsertAccount(Person user, string accountName)
        {
            if (user.VerifyExistingAccount(accountName))
            {
                MenuDefault.Message("You already have an account " + accountName);
                AddAccount(user);
                return;
            }
            int idAccount = 0;
            if (!user.AccountsEmpty())
            {
                idAccount = user.Accounts.Max(t => t.AccountId);
            }
            idAccount++;
            user.Accounts.Add(new Account(idAccount, accountName));
            UpdateUser(user);
            ManageAccounts(user);
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
            user.Accounts = new List<Account>();
            Collection.InsertOne(user);
        }

        private static void UpdateUser(Person user)
        {
            var filter = Builders<Person>.Filter.Eq("_id", user.Id);
            var update = Builders<Person>.Update.Set("accounts", user.Accounts);
            Collection.UpdateOne(filter, update);
        }

        private static void DeleteUser(Person user)
        {
            var filter = Builders<Person>.Filter.Eq("_id", user.Id);
            Collection.DeleteOne(filter);
            Login(Collection);
        }
    }
}
