using DebuggingAndRefactoringTask1.Models;

namespace DebuggingAndRefactoringTask1.Repository
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccounts();
        Account GetAccount(string id);
        void CreateAccount(Account account);
        void UpdateBalance(string id, double amount, bool isDeposit);
    }
}
