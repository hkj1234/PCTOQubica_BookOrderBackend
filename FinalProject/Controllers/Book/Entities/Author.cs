using System.ComponentModel.DataAnnotations;

namespace FinalProject.Controllers.Book.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string AuthorName { get; set; }
    }
}
