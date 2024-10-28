using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProject.Core.Author.Interfaces;
using FinalProject.Core.Book.Entities;
using FinalProject.Core.Book.Exceptions;
using FinalProject.Core.Book.Interfaces;
using FinalProject.Core.BookCategory.Interfaces;
using FinalProject.Core.Customer.Entities;
using FinalProject.Core.Customer.Exceptions;
using FinalProject.Core.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Core.Book
{
    public class BookManager : IBookManager
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IBookCategoriesRepository _bookCategoriesRepository;
        public BookManager(IBooksRepository booksRepository, IAuthorsRepository authorsRepository, IBookCategoriesRepository bookCategoriesRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
            _bookCategoriesRepository = bookCategoriesRepository;
        }
        public async Task<IEnumerable<BookResult>> GetBooksAsync()
        {
            return await _booksRepository.GetBooksAsync();
        }
        public async Task<IEnumerable<BookResult>> GetBooksWithFilterAsync(BookResultWithoutId b)
        {
            return await _booksRepository.GetBooksWithFilterAsync(b);
        }
        public async Task PostBookAsync(BookToUpdate b)
        {
            //veidmao se b.CategoryId e b.AuthorId esistono
            if (!(await _bookCategoriesRepository.ExistBookCategories(b.CategoryId)))
                throw new BookCategorieDoesntExistException();

            if (!(await _authorsRepository.ExistAuthors(b.AuthorId)))
                throw new AuthorDoesntExistException();


            await _booksRepository.PostBookAsync(b);
        }
    }
}
