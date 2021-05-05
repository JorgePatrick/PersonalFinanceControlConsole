using PersonalFinanceControlConsole.Controlers;

namespace PersonalFinanceControlConsole.Menus
{
    internal class MenuAccount
    {
        private readonly PersonControler personControler;
        private readonly AccountListControler accountListControler;
        private readonly AccountControler accountControler;
        private readonly int AccountId;

        public MenuAccount(PersonControler personControler, AccountListControler accountListControler, int accountId)
        {
            this.personControler = personControler;
            this.accountListControler = accountListControler;
            AccountId = accountId;
            accountControler = new AccountControler(accountListControler.GetAccountObj(AccountId));
        }

        public void AccountTransactions()
        {
            if (accountListControler.AccountsEmpty())
                return;
            
            var title = "Account Transactions " + accountControler.GetAccountName();
            string[] optionList =
               {
                "Delete Account",
                "Account Infos"
               };
            string option = MenuDefaults.ListScreen(title, "Option", optionList, () => AccountTransactions());
            switch (option)
            {
                case "1": DeleteAccount(); break;
                case "2": ShowAccount(); break;
                case MenuStructure.Back: break;
                default: AccountTransactions(); break;
            }
        }

        private void DeleteAccount()
        {
            if (MenuDefaults.AreYouSureScreen("Delete Account", () => DeleteAccount()))
                accountListControler.DeleteAccount(personControler.GetId(), AccountId);

            AccountTransactions();
        }

        private void ShowAccount()
        {
            string[,] accounInfo = accountControler.GetAccountInfo();
            MenuOptions.ShowAccount(accounInfo);
            AccountTransactions();
        }
    }
}