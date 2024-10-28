using Moq;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Core.Customer;
using Microsoft.Extensions.Configuration;
using FinalProject.Core.Customer.Entities;
using FinalProject.Core.Customer.Exceptions;
using FinalProject.Database.Customer.Entities;
using FinalProject;

namespace FinalProjectNUnitTest
{
    public class CustomerManagerTest
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ICustomersRepository> _mockCustomersRepository;
        public CustomerManagerTest()
        {
            _mockCustomersRepository = new Mock<ICustomersRepository>(MockBehavior.Strict);
            _mockConfiguration = new Mock<IConfiguration>(MockBehavior.Strict);
        }

        [Test]
        public void RegisterWithTwoPasswordsDontMatch()
        {
            var TwoPasswordsDoesntMatchAccount = new CustomerRegister
            {
                Email = "test",
                Password = "test",
                VerifyPassword = "wrong"
            };
            var manager = new CustomerManager(_mockConfiguration.Object, _mockCustomersRepository.Object);

            async Task Act()
            {
                await manager.RegisterAsync(TwoPasswordsDoesntMatchAccount);
            }

            Assert.ThrowsAsync<WrongEmailOrPasswordException>(Act);
        }

        [Test]
        public void RegisterWithExistingEmail()
        {
            var ExistingEmailAccount = new CustomerRegister
            {
                Email = "test",
                Password = "test",
                VerifyPassword = "test"
            };
            var manager = new CustomerManager(_mockConfiguration.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(ExistingEmailAccount.Email)).ReturnsAsync(true);

            async Task Act()
            {
                await manager.RegisterAsync(ExistingEmailAccount);
            }

            Assert.ThrowsAsync<ExisitingEmailException>(Act);
        }

        [Test]
        public void RegisterWithNewAccount()
        {
            var NewAccount = new CustomerRegister
            {
                Email = "wrong",
                Password = "test",
                VerifyPassword = "test"
            };
            var manager = new CustomerManager(_mockConfiguration.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(NewAccount.Email)).ReturnsAsync(false);
            _mockCustomersRepository.Setup(x => x.AddNewCustomer(NewAccount)).Returns(Task.CompletedTask);

            async Task Act()
            {
                await manager.RegisterAsync(NewAccount);
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void NotExistingAccountLogin()
        {
            var notExistingAccount = new CustomerLogin
            {
                Email = "notExisting",
                Password = "test"
            };
            var manager = new CustomerManager(_mockConfiguration.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.FirstOrDefaultCustomer(notExistingAccount.Email)).ReturnsAsync(new DBCustomer());

            async Task Act()
            {
                await manager.LoginAsync(notExistingAccount);
            }

            Assert.ThrowsAsync<WrongEmailOrPasswordException>(Act);
        }

        [Test]
        public void WrongPasswordLogin()
        {
            var wrongPasswordAccount = new CustomerLogin
            {
                Email = "test",
                Password = "wrong"
            };
            var manager = new CustomerManager(_mockConfiguration.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.FirstOrDefaultCustomer(wrongPasswordAccount.Email)).ReturnsAsync(new DBCustomer
            {
                Password = "test",
            });

            async Task Act()
            {
                await manager.LoginAsync(wrongPasswordAccount);
            }

            Assert.ThrowsAsync<WrongEmailOrPasswordException>(Act);
        }

        [Test]
        public void SuccessLogin()
        {
            var SuccessAccount = new CustomerLogin
            {
                Email = "test",
                Password = "test"
            };
            var manager = new CustomerManager(_mockConfiguration.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.FirstOrDefaultCustomer(SuccessAccount.Email)).ReturnsAsync(new DBCustomer
            {
                Password = "test",
            });

            async Task Act()
            {
                await manager.LoginAsync(SuccessAccount);
            }

            Assert.DoesNotThrowAsync(Act);
        }
    }
}