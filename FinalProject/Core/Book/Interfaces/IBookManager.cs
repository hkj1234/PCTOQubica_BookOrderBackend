using FinalProject.Core.Book.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Core.Book.Interfaces
{
    public interface IBookManager
    {
        public Task<IEnumerable<BookResult>> GetBooksAsync();
        public Task<IEnumerable<BookResult>> GetBooksWithFilterAsync(BookResultWithoutId b);
        public Task PostBookAsync(BookToUpdate b);
    }
}
