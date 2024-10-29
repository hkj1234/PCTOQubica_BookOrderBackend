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
        public int Amount { get; set; }

        [ForeignKey("Customer")]
        public string? CustomerEmail { get; set; }
        public DBCustomer? Customer { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public DBBook? Book { get; set; }
        public DateTime OrderDateTime { get; set; }

    }
}
