using System.ComponentModel.DataAnnotations;

namespace FinalProject.Database.Author.Entities
{
    public class DBAuthor
    {
        [Key]
        public int Id { get; set; }
        public string AuthorName { get; set; } = "";
    }
}
