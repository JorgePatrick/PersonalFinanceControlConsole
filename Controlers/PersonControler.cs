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
        public Person User;

        internal bool UserNotExists(int userId)
        {
            User = PersonDataBase.ReadUser(userId);
            return NotExists();
        }

        private bool NotExists()
        {
            if (User == null)
            {
                return true;
            }
            return false;
        }

        internal string GetUserName()
        {
            return User.Name;
        }

        internal void InsertUser(int userId, string userName)
        {
            User = new Person(userId, userName);
            PersonDataBase.InsertUser(User);
        }

        internal void DeleteUser()
        {
            PersonDataBase.DeleteUser(User);
        }

        internal List<Account> GetAccounts()
        {
            return User.Accounts;
        }

        internal BsonObjectId GetId()
        {
            return User.Id;
        }

        internal void UpdateName(string newName)
        {
            User.Name = newName;
            PersonDataBase.UpdateName(User);
        }
    }
}