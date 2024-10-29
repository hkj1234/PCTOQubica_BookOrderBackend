using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace FinalProject.Database.Customer.Entities
{
    public class DBCustomer
    {
        [Key]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}