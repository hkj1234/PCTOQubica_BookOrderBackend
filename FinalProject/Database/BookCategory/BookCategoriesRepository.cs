using FinalProject.Core.BookCategory.Interfaces;
using FinalProject.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Database.BookCategory
{
    public class BookCategoriesRepository : IBookCategoriesRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public BookCategoriesRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<bool> ExistBookCategories(int id)
        {
            await using var _context = _dbContextFactory.CreateDbContext();
            return await _context.BookCategories.AnyAsync(x => x.Id == id);
        }
    }
}
