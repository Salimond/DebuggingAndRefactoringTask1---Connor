using DebuggingAndRefactoringTask1.Models;

namespace DebuggingAndRefactoringTask1.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private static List<Account> accounts;

        public AccountRepository()
        {
            accounts = new List<Account>();
        }

        /// <summary>
        /// Retrieve all accounts currently stored in the repo
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAllAccounts()
        {
            return accounts;
        }

        /// <summary>
        /// Retrieve one specific account via id
        /// </summary>
        /// <param name="id">The id of the account</param>
        /// <returns></returns>
        public Account GetAccount(string id)
        {
            return accounts.Where(_ => _.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Add an account to the repo based off a passed in object
        /// </summary>
        /// <param name="account">The account to be created</param>
        /// <exception cref="Exception">Returned if the ID is already in use</exception>
        public void CreateAccount(Account account)
        {
            if (accounts.Any(_ => _.Id == account.Id))
            {
                throw new Exception("ID is already in use.");
            }
            accounts.Add(account);
        }
        
        /// <summary>
        /// Modifies the balance of the account, both for adding and withdrawing funds
        /// </summary>
        /// <param name="id">The id of the account to be modified</param>
        /// <param name="balance">The change in balance</param>
        /// <param name="isDeposit">True if funds are being added, false if they're being removed</param>
        /// <exception cref="Exception"></exception>

        public void UpdateBalance(string id, double balance, bool isDeposit)
        {
            var accountToUpdate = GetAccount(id);

            if (accountToUpdate == null)
            {
                throw new Exception("No account found with that ID");
            }

            if (isDeposit)
            {
                accountToUpdate.Balance += balance;
            }
            else
            {
                if (accountToUpdate.Balance >= balance)
                {
                    accountToUpdate.Balance -= balance;
                }
                else
                {
                    throw new Exception("Insufficient balance");
                }

            }
        }
    }
}
