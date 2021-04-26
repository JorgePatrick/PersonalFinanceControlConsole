using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace PersonalFinanceControlConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var menuHandler = new Menus.MenuHandler();
            menuHandler.Show();
        }
    }
}
