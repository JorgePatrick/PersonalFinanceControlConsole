using PersonalFinanceControlConsole.Controlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Menus
{
    class MenuProfile
    {
        private readonly PersonControler personControler;

        public MenuProfile(PersonControler personControler)
        {
            this.personControler = personControler;
        }

        public void ManageProfile()
        {
            var title = "Manage Profile " + personControler.GetUserName();
            string[] optionList =
               {
                "Change Name",
                "Delete User"
               };
            string option = MenuDefaults.ListScreen(title, "Option", optionList, () => ManageProfile());
            switch (option)
            {
                case "1": ChangeName(); break;
                case "2": DeleteUser(); break;
                case MenuStructure.Back: break;
                default: ManageProfile(); break;
            }
        }
        private void ChangeName()
        {
            string newName = "invalid";
            while (newName == "invalid")
            {
                newName = Menus.MenuOptions.ChangeName(personControler.GetUserName());
            }
            switch (newName)
            {
                case MenuStructure.Back: ManageProfile(); break;
                default: UpdateName(newName); break;
            }
        }

        private void DeleteUser()
        {
            if (MenuDefaults.AreYouSureScreen("Delete User", () => DeleteUser()))
            {
                personControler.DeleteUser();
            }
            else
            {
                ManageProfile();
            }
        }

        private void UpdateName(string newName)
        {
            if (MenuDefaults.AreYouSureScreen("Change Name", () => UpdateName(newName)))
            {
                personControler.UpdateName(newName);
            }
            ManageProfile();
        }
    }
}
