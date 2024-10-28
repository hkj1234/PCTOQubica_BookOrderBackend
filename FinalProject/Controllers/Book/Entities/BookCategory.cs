using System.ComponentModel.DataAnnotations;

namespace FinalProject.Controllers.Book.Entities
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
