using FinalProject.Controllers.Book.Entities;
using FinalProject.Core.Book.Entities;
using FinalProject.Core.Book.Interfaces;
using FinalProject.Database.Book.Entities;
using FinalProject.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Database.Book
{
    public class BooksRepository : IBooksRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public BooksRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<BookResult>> GetBooksAsync()
        {
            await using var _context = await _dbContextFactory.CreateDbContext();
            return await _context.Books
                .Include(p => p.BookCategory)
                .Include(p => p.Author)
                .Select(p => new BookResult
                {
                    Id = p.Id,
                    Title = p.Title,
                    AuthorName = p.Author.AuthorName,
                    BookCategory = p.BookCategory.CategoryName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookResult>> GetBooksWithFilterAsync(BookResultWithoutId b)
        {
            await using var _context = await _dbContextFactory.CreateDbContext();
            return await _context.Books
                    .Include(p => p.BookCategory)
                    .Include(p => p.Author)
                    .Select(p => new BookResult
                    {
                        Id = p.Id,
                        Title = p.Title,
                        AuthorName = p.Author.AuthorName,
                        BookCategory = p.BookCategory.CategoryName
                    })
                    .Where(p => string.IsNullOrEmpty(b.AuthorName) || p.AuthorName.Contains(b.AuthorName))
                    .Where(p => string.IsNullOrEmpty(b.Title) || p.Title.Contains(b.Title))
                    .Where(p => string.IsNullOrEmpty(b.BookCategory) || p.BookCategory.Contains(b.BookCategory))
                    .ToListAsync();
        }
        public async Task PostBookAsync(BookToUpdate b)
        {
            await using var _context = await _dbContextFactory.CreateDbContext();
            DBBook newBook = new DBBook
            {
                Title = b.Title,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId,
            };

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
        }
    }
}
