using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
