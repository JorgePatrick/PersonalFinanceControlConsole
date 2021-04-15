using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole
{
    class MenuLoginRegister
    {
        public static Person Login()
        {
            var user = WriteLogin();
            if (user.CheckIfExists())
            {
                Register(user);
            }
            return user;
        }
        private static Person WriteLogin()
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Login");
            MenuDefault.WriteLine("Type your Id: ");
            int userId;
            bool userIdIsNumber = int.TryParse(Console.ReadLine(), out userId);
            if (!userIdIsNumber)
            {
                WriteLogin();
            }
            MenuDefault.CheckExit(userId);
            Person user = Menu.ReadUser(userId);
            if (user == null)
            {
                user = new Person();
                user.UserId = userId;
            }
            return user;
        }
        private static void Register(Person user)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Register");
            MenuDefault.WriteLine("Id: " + user.UserId);
            MenuDefault.WriteLine("Enter your name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Register(user);
            }
            if (name == "0")
            {
                MenuDefault.CheckExit(0);
            }
            user.Name = name;
            Menu.Save(user);
        }
    }
}
