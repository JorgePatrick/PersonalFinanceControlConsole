using PersonalFinanceControlConsole.DataBases;
using PersonalFinanceControlConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace PersonalFinanceControlConsole.Controlers
{
    internal class PersonControler
    {
        public PeopleDataBase PersonDataBase = new PeopleDataBase();
        public Person user;

        internal bool UserNotExists(int userId)
        {
            user = PersonDataBase.ReadUser(userId);
            return NotExists();
        }

        private bool NotExists()
        {
            if (user == null)
            {
                return true;
            }
            return false;
        }

        internal string GetUserName()
        {
            return user.Name;
        }

        internal void InsertUser(int userId, string userName)
        {
            user = new Person(userId, userName);
            PersonDataBase.InsertUser(user);
        }

        internal void DeleteUser()
        {
            PersonDataBase.DeleteUser(user);
            var menuHandler = new Menus.MenuHandler();
            menuHandler.Show();
        }

        internal List<Account> GetAccounts()
        {
            return user.Accounts;
        }

        internal BsonObjectId GetId()
        {
            return user.Id;
        }
    }
}