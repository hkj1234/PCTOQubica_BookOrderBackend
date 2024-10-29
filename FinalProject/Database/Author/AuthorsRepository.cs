using FinalProject.Core.Author.Interfaces;
using FinalProject.Core.Book.Entities;
using FinalProject.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FinalProject.Database.Author
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public AuthorsRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<bool> ExistAuthors(int id)
        {
            await using var _context = _dbContextFactory.CreateDbContext();
            return await _context.Authors.AnyAsync(x => x.Id == id);
        }
    }
}
