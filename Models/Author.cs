using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Author
    { 
        [Key]
        public int Id { get; set; }
        public string AuthorName { get; set; }
    }
}
