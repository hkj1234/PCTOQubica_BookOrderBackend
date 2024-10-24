using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Controllers.Book.Entities
{
    public class BookDB
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [ForeignKey("BookCategory")]
        public int CategoryId { get; set; }
        public BookCategory BookCategory { get; set; }
    }

    public class BookResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string BookCategory { get; set; }
    }

    public class BookResultWithoutId
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string BookCategory { get; set; }
    }

    public class BookToUpdate
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
