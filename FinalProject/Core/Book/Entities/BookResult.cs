using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Core.Book.Entities
{
    public class BookResult
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string BookCategory { get; set; }
    }

    public class BookResultWithoutId
    {
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string BookCategory { get; set; }
    }

    public class BookToUpdate
    {
        public required string Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
