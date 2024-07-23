using DebuggingAndRefactoringTask1.Repository;
using DebuggingAndRefactoringTask1.Models;

namespace BankingSystem
{
    class Program
    {
        static IAccountRepository AccountRepository = new AccountRepository();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Add Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Display Account Details");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAccount();
                        break;
                    case "2":
                        DepositMoney();
                        break;
                    case "3":
                        WithdrawMoney();
                        break;
                    case "4":
                        DisplayAccountDetails();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }


        //Adds an account after checking to ensure fields are filled in.
        static void AddAccount()
        {
            Console.WriteLine("Enter Account ID:");
            var id = Console.ReadLine();

            if (id == "")
            {
                Console.WriteLine("ID cannot be empty.");
                return;
            }

            Console.WriteLine("Enter Account Holder Name:");
            var name = Console.ReadLine();

            if (name == "")
            {
                Console.WriteLine("Name cannot be empty");
                return;
            }

            Account account = new Account(id, name);
            try
            {
                AccountRepository.CreateAccount(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Account added successfully.");
        }

        //Adds funds to an account after checking the ID is filled in and the balance is in a valid format.
        static void DepositMoney()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            if (id == "")
            {
                Console.WriteLine("ID cannot be empty.");
                return;
            }

            Console.WriteLine("Enter Amount to Deposit:");
            bool amountSuccess = double.TryParse(Console.ReadLine(), out double amount);

            if (!amountSuccess)
            {
                Console.WriteLine("Balance must be entered in a valid format.");
                return;
            }

            try
            {
                AccountRepository.UpdateBalance(id, amount, true);
                Console.WriteLine("Balance added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Removes funds from an account after checking the ID is filled in and the balance is in a valid format.
        static void WithdrawMoney()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            if (id == "")
            {
                Console.WriteLine("ID cannot be empty.");
                return;
            }

            Console.WriteLine("Enter Amount to Withdrawn:");
            bool amountSuccess = double.TryParse(Console.ReadLine(), out double amount);

            if (!amountSuccess)
            {
                Console.WriteLine("Balance must be entered in a valid format.");
                return;
            }

            try
            {
                AccountRepository.UpdateBalance(id, amount, false);
                Console.WriteLine("Balance withdrawn successfully.");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        //Retrieves details for an account based off of the ID of the user.
        static void DisplayAccountDetails()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            if (id == "")
            {
                Console.WriteLine("ID cannot be empty.");
                return;
            }

            var displayAccount = AccountRepository.GetAccount(id);

            if (displayAccount == null)
            { 
                Console.WriteLine("Account not found.");
                return;
            }

            Console.WriteLine($"Account ID: {displayAccount.Id}");
            Console.WriteLine($"Account Holder: {displayAccount.Name}");
            Console.WriteLine($"Balance: {displayAccount.Balance}");
        }
    }

}
