using System.ComponentModel.DataAnnotations;

namespace FinalProject.Core.Book.Entities
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }
    }
}
