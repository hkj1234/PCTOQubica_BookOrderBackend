using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Core.Book.Entities
{
    public class BookResult
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public string? BookCategory { get; set; }
    }

    public class BookResultWithoutId
    {
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public string? BookCategory { get; set; }
    }

    public class BookToUpdate
    {
        public string? Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
