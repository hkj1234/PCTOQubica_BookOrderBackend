using System.ComponentModel.DataAnnotations;

namespace FinalProject.Controllers.Book.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public required string AuthorName { get; set; }
    }
}
