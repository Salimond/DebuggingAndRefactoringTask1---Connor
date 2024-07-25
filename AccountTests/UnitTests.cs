using BankingSystem;
using DebuggingAndRefactoringTask1.Models;
using DebuggingAndRefactoringTask1.Repository;

namespace AccountTests
{
    [TestClass]
    public class UnitTests
    {
        private IAccountRepository _accountRepository;

        readonly Account testAccount1 = new Account("123", "Connor");
        readonly Account testAccount2 = new Account("321", "Connor2");
        readonly Account testAccountWithBalance = new Account("321", "Connor2", 1000);


        [TestInitialize]
        public void Setup()
        {
            _accountRepository = new AccountRepository();
        }

        [TestMethod]
        public void CreateAccount_ValidAccount()
        {
            //test that account is successfully created when none exist currently
            _accountRepository.CreateAccount(testAccount1);
            Assert.IsTrue(_accountRepository.GetAllAccounts().Count() == 1);

            //test that an account is created fine when there's already accounts in the system
            _accountRepository.CreateAccount(testAccount2);
            Assert.IsTrue(_accountRepository.GetAllAccounts().Count() == 2);
        }

        [TestMethod]
        public void CreateAccount_DuplicateAccount()
        {
            _accountRepository.CreateAccount(testAccount1);
            Assert.IsTrue(_accountRepository.GetAllAccounts().Count() == 1);

            //when attempting to create an account with the same id, CreateAccount should return an error and not insert the account
            try
            {
                _accountRepository.CreateAccount(testAccount1);
            }
            catch
            {
                
            }
            Assert.IsTrue(_accountRepository.GetAllAccounts().Count() == 1);
        }

        [TestMethod]
        public void GetAccount_ReturnsAccount()
        {
            //ensure we're retrieving the account when the id is supplied
            _accountRepository.CreateAccount(testAccount1);
            var returnTestAccount = _accountRepository.GetAccount(testAccount1.Id);

            Assert.IsNotNull(returnTestAccount);
            Assert.AreEqual(returnTestAccount, testAccount1);
        }

        [TestMethod]
        public void GetAccount_DoesntReturnOnFailedSearch()
        {
            //ensure that null is being returned when the id doesn't match any account
            _accountRepository.CreateAccount(testAccount1);
            var returnTestAccount = _accountRepository.GetAccount(testAccount2.Id);

            Assert.IsNull(returnTestAccount);
        }

        [TestMethod]
        public void UpdateBalance_AddFundsSuccess()
        {
            //Create an account, add 500 to the balance and make sure that the all the details match up with what we'd expect
            _accountRepository.CreateAccount(testAccount1);
            double balanceToAdd = 500;
            Account comparisonAccount = new Account(testAccount1.Id, testAccount1.Name, balanceToAdd);
            _accountRepository.UpdateBalance(testAccount1.Id, balanceToAdd, true);
            
            var returnTestAccount = _accountRepository.GetAccount(testAccount1.Id);

            Assert.IsNotNull(returnTestAccount);
            Assert.AreEqual(returnTestAccount.Id, comparisonAccount.Id);
            Assert.AreEqual(returnTestAccount.Name, comparisonAccount.Name);
            Assert.AreEqual(returnTestAccount.Balance, comparisonAccount.Balance, 0.001);

        }

        [TestMethod]
        public void UpdateBalance_RemoveFundsSuccess()
        {
            //check the correct amount is removed from an account when balance is removed
            _accountRepository.CreateAccount(testAccountWithBalance);
            double balanceToRemove = 500;
            Account comparisonAccount = new Account(testAccountWithBalance.Id, testAccountWithBalance.Name, testAccountWithBalance.Balance - balanceToRemove);
            _accountRepository.UpdateBalance(testAccountWithBalance.Id, balanceToRemove, false);       
            var returnTestAccount = _accountRepository.GetAccount(testAccountWithBalance.Id);

            Assert.IsNotNull(returnTestAccount);
            Assert.AreEqual(returnTestAccount.Id, comparisonAccount.Id);
            Assert.AreEqual(returnTestAccount.Name, comparisonAccount.Name);
            Assert.AreEqual(returnTestAccount.Balance, comparisonAccount.Balance, 0.001);
        }

        [TestMethod]
        public void UpdateBalance_RemovingInvalidFunds()
        {
            //make sure an exception is thrown when more funds are attempted to be removed than are available
            _accountRepository.CreateAccount(testAccountWithBalance);
            double balanceToRemove = testAccountWithBalance.Balance + 1;

            try
            {
                _accountRepository.UpdateBalance(testAccountWithBalance.Id, balanceToRemove, false);
                Assert.Fail();
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void UpdateBalance_NoMatchingAccount()
        {
            //If there's no matching account, an exception is thrown and no funds are added anywhere
            //Very messy, would like to go and improve return types from Repo if had time but this will do for now.
            _accountRepository.CreateAccount(testAccount1);
            double balanceToAdd = 500;

            try
            {
                _accountRepository.UpdateBalance(testAccount2.Id, balanceToAdd, true);
                Assert.Fail();
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {

            }
        }
    }
}