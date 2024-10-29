using FinalProject.Core.Book.Entities;
using FinalProject.Core.Book.Exceptions;
using FinalProject.Core.Book;
using FinalProject.Core.BookCategory.Interfaces;
using FinalProject.Core.Order.Interfaces;
using Moq;
using FinalProject.Core.Order;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Core.Customer.Exceptions;
using FinalProject.Core.Order.Entities;
using FinalProject.Core.Order.Exeptions;

namespace FinalProjectNUnitTest
{
    public class OrderManagerTest
    {
        private readonly Mock<IOrdersRepository> _mockOrdersRepository;
        private readonly Mock<IBookCategoriesRepository> _mockBookCategoriesRepository;
        private readonly Mock<ICustomersRepository> _mockCustomersRepository;
        public OrderManagerTest()
        {
            _mockOrdersRepository = new Mock<IOrdersRepository>(MockBehavior.Strict);
            _mockBookCategoriesRepository = new Mock<IBookCategoriesRepository>(MockBehavior.Strict);
            _mockCustomersRepository = new Mock<ICustomersRepository>(MockBehavior.Strict);
        }

        [Test]
        public void GetPesonalOrdersSucess()
        {
            var email = "test@gmail.com";
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(email)).ReturnsAsync(true);
            _mockOrdersRepository.Setup(x => x.GetPesonalOrdersAsync(email)).ReturnsAsync(new List<OrderResult>());

            async Task Act()
            {
                await manager.GetPesonalOrdersAsync(email);
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void GetPesonalOrdersAsyncWithWrongId()
        {
            var email = "test@gmail.com";
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(email)).ReturnsAsync(false);

            async Task Act()
            {
                await manager.GetPesonalOrdersAsync(email);
            }

            Assert.ThrowsAsync<CustomerDorsntExistException>(Act);
        }

        [Test]
        public void GetPesonalOrdersAsyncWithNullId()
        {
            string email = null;
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);

            async Task Act()
            {
                await manager.GetPesonalOrdersAsync(email);
            }

            Assert.ThrowsAsync<CustomerDorsntExistException>(Act);
        }

        [Test]
        public void PostOrderWithWrongEmail()
        {
            var email = "test@gmail.com";
            var orderToCreate = new OrderToCreate
            {
                Amount = 1,
                BookId = 1,
            };
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(email)).ReturnsAsync(false);

            async Task Act()
            {
                await manager.PostOrderAsync(email, orderToCreate);
            }

            Assert.ThrowsAsync<CustomerDorsntExistException>(Act);
        }

        [Test]
        public void PostOrderWithNegativeAmount()
        {
            var email = "test@gmail.com";
            var orderToCreate = new OrderToCreate
            {
                Amount = -1,
                BookId = 1,
            };
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(email)).ReturnsAsync(true);

            async Task Act()
            {
                await manager.PostOrderAsync(email, orderToCreate);
            }

            Assert.ThrowsAsync<NegativeOrNullAmountException>(Act);
        }

        [Test]
        public void PostOrderWithNullAmount()
        {
            var email = "test@gmail.com";
            var orderToCreate = new OrderToCreate
            {
                BookId = 1,
            };
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(email)).ReturnsAsync(true);

            async Task Act()
            {
                await manager.PostOrderAsync(email, orderToCreate);
            }

            Assert.ThrowsAsync<NegativeOrNullAmountException>(Act);
        }

        [Test]
        public void PostOrderWithZeroAmount()
        {
            var email = "test@gmail.com";
            var orderToCreate = new OrderToCreate
            {
                Amount = 0,
                BookId = 1,
            };
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(email)).ReturnsAsync(true);

            async Task Act()
            {
                await manager.PostOrderAsync(email, orderToCreate);
            }

            Assert.ThrowsAsync<NegativeOrNullAmountException>(Act);
        }

        [Test]
        public void PostOrderWithBookCategoryInexisting()
        {
            var email = "test@gmail.com";
            var orderToCreate = new OrderToCreate
            {
                Amount = 1,
                BookId = -1,
            };
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(email)).ReturnsAsync(true);
            _mockBookCategoriesRepository.Setup(x => x.ExistBookCategories(-1)).ReturnsAsync(false);

            async Task Act()
            {
                await manager.PostOrderAsync(email, orderToCreate);
            }

            Assert.ThrowsAsync<BookCategorieDoesntExistException>(Act);
        }

        [Test]
        public void PostOrderSuccess()
        {
            var email = "test@gmail.com";
            var orderToCreate = new OrderToCreate
            {
                Amount = 1,
                BookId = 1,
            };
            var manager = new OrderManager(_mockOrdersRepository.Object, _mockBookCategoriesRepository.Object, _mockCustomersRepository.Object);
            _mockCustomersRepository.Setup(x => x.ExistCustomer(email)).ReturnsAsync(true);
            _mockBookCategoriesRepository.Setup(x => x.ExistBookCategories(1)).ReturnsAsync(false);
            _mockOrdersRepository.Setup(x => x.PostOrderAsync(email, orderToCreate)).Returns(Task.CompletedTask);

            async Task Act()
            {
                await manager.PostOrderAsync(email, orderToCreate);
            }

            Assert.DoesNotThrowAsync(Act);
        }
    }
}