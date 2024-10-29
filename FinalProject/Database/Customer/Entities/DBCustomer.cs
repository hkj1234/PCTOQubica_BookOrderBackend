using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace FinalProject.Database.Customer.Entities
{
    public class DBCustomer
    {
        [Key]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}