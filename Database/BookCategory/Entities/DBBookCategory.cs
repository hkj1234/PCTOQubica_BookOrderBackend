using System.ComponentModel.DataAnnotations;

namespace FinalProject.Database.BookCategory.Entities
{
    public class DBBookCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
