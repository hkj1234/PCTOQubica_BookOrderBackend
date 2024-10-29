using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Database.Book.Entities;
using FinalProject.Database.Customer.Entities;
using Microsoft.VisualBasic;

namespace FinalProject.Database.Order.Entities
{
    public class DBOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public required string CustomerEmail { get; set; }
        public DBCustomer Customer { get; set; } = new DBCustomer();

        [ForeignKey("Book")]
        [Required]
        public int BookId { get; set; }
        public DBBook Book { get; set; } = new DBBook();
        public DateTime OrderDateTime { get; set; }

    }
}
