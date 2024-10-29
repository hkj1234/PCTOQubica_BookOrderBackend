using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Database.Author.Entities;
using FinalProject.Database.BookCategory.Entities;

namespace FinalProject.Database.Book.Entities
{
    public class DBBook
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";

        [ForeignKey("Author")]
        [Required]
        public int AuthorId { get; set; }
        public DBAuthor Author { get; set; } = new DBAuthor();

        [ForeignKey("BookCategory")]
        [Required]
        public int CategoryId { get; set; }
        public DBBookCategory BookCategory { get; set; } = new DBBookCategory();
    }
}
