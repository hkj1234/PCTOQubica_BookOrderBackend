using Moq;
using FinalProject.Core.Author.Interfaces;
using FinalProject.Core.Book.Interfaces;
using FinalProject.Core.BookCategory.Interfaces;
using FinalProject.Core.Customer.Entities;
using FinalProject.Core.Customer.Exceptions;
using FinalProject.Core.Customer;
using FinalProject.Core.Book;
using FinalProject.Core.Book.Entities;
using FinalProject.Core.Book.Exceptions;

namespace FinalProjectNUnitTest
{
    public class BookManagerTest
    {
        private readonly Mock<IBooksRepository> _mockBooksRepository;
        private readonly Mock<IAuthorsRepository> _mockAuthorsRepository;
        private readonly Mock<IBookCategoriesRepository> _mockBookCategoriesRepository;
        public BookManagerTest()
        {
            _mockBooksRepository = new Mock<IBooksRepository>(MockBehavior.Strict);
            _mockAuthorsRepository = new Mock<IAuthorsRepository>(MockBehavior.Strict);
            _mockBookCategoriesRepository = new Mock<IBookCategoriesRepository>(MockBehavior.Strict);
        }

        [Test]
        public void GetBookWithFilterSuccess()
        {
            var bookResultWithoutId = new BookResultWithoutId
            {
                Title = "test",
                AuthorName = "test",
                BookCategory = "test",
            };
            var manager = new BookManager(_mockBooksRepository.Object, _mockAuthorsRepository.Object, _mockBookCategoriesRepository.Object);
            _mockBooksRepository.Setup(x => x.GetBooksWithFilterAsync(bookResultWithoutId)).ReturnsAsync(new List<BookResult>());

            async Task Act()
            {
                await manager.GetBooksWithFilterAsync(bookResultWithoutId);
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void GetBookSuccess()
        {
            var manager = new BookManager(_mockBooksRepository.Object, _mockAuthorsRepository.Object, _mockBookCategoriesRepository.Object);
            _mockBooksRepository.Setup(x => x.GetBooksAsync()).ReturnsAsync(new List<BookResult>());

            async Task Act()
            {
                await manager.GetBooksAsync();
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void PostBookWithInexistingBookCategory()
        {
            var BookWithInexistingBookCategory = new BookToUpdate
            {
                Title = "test",
                AuthorId = 1,
                CategoryId = -1,
            };
            var manager = new BookManager(_mockBooksRepository.Object, _mockAuthorsRepository.Object, _mockBookCategoriesRepository.Object);
            
            
            _mockBookCategoriesRepository.Setup(x => x.ExistBookCategories(-1)).ReturnsAsync(false);
            _mockAuthorsRepository.Setup(x => x.ExistAuthors(1)).ReturnsAsync(true);

            async Task Act()
            {
                await manager.PostBookAsync(BookWithInexistingBookCategory);
            }

            Assert.ThrowsAsync<BookCategorieDoesntExistException>(Act);
        }

        [Test]
        public void PostBookWithInexistingAuthor()
        {
            var BookWithInexistingAuthor = new BookToUpdate
            {
                Title = "test",
                AuthorId = -1,
                CategoryId = 1,
            };
            var manager = new BookManager(_mockBooksRepository.Object, _mockAuthorsRepository.Object, _mockBookCategoriesRepository.Object);
            _mockAuthorsRepository.Setup(x => x.ExistAuthors(-1)).ReturnsAsync(false);
            _mockBookCategoriesRepository.Setup(x => x.ExistBookCategories(1)).ReturnsAsync(true);

            async Task Act()
            {
                await manager.PostBookAsync(BookWithInexistingAuthor);
            }

            Assert.ThrowsAsync<AuthorDoesntExistException>(Act);
        }

        [Test]
        public void PostBookGoodResult()
        {
            var newBook = new BookToUpdate
            {
                Title = "test",
                AuthorId = 1,
                CategoryId = 1,
            };
            var manager = new BookManager(_mockBooksRepository.Object, _mockAuthorsRepository.Object, _mockBookCategoriesRepository.Object);
            _mockBookCategoriesRepository.Setup(x => x.ExistBookCategories(1)).ReturnsAsync(true);
            _mockAuthorsRepository.Setup(x => x.ExistAuthors(1)).ReturnsAsync(true);
            _mockBooksRepository.Setup(x => x.PostBookAsync(newBook)).Returns(Task.CompletedTask);

            async Task Act()
            {
                await manager.PostBookAsync(newBook);
            }

            Assert.DoesNotThrowAsync(Act);
        }
    }
}