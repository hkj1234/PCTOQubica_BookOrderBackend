using FinalProject.Core.Book.Entities;

namespace FinalProject.Core.Book.Interfaces
{
    public interface IBooksRepository
    {
        public Task<IEnumerable<BookResult>> GetBooksAsync();
        public Task<IEnumerable<BookResult>> GetBooksWithFilterAsync(BookResultWithoutId b);
        public Task PostBookAsync(BookToUpdate b);
    }
}
