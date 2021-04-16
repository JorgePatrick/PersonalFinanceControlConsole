using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Menus
{
    class MenuOptions
    {
        public static int WriteLogin()
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Login");
            MenuDefault.WriteLine("Type your Id: ");
            string userIdStr = MenuDefault.ReadLine(null, Menus.Enums.ETypeRead.Int);
            if (userIdStr == "invalid")
            {
                return 0;
            }
            return int.Parse(userIdStr);
        }
        public static void Register(Person user)
        {
            MenuDefault.DrawScreen();
            MenuDefault.SetTitle("Register");
            MenuDefault.WriteLine("Id: " + user.UserId);
            MenuDefault.WriteLine("Enter your name: ");
            string name = MenuDefault.ReadLine(null, Menus.Enums.ETypeRead.String);
            if (name != "invalid")
            {
                user.Name = name;
                Menu.Save(user);
            }
        }
    }
}
