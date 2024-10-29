using System.ComponentModel.DataAnnotations;

namespace FinalProject.Core.Book.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string? AuthorName { get; set; }
    }
}
