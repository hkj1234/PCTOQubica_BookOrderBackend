using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Core.Customer.Entities;
using FinalProject.Database.Book.Entities;
using Microsoft.VisualBasic;

namespace FinalProject.Core.Order.Entities
{
    public class OrderToCreate
    {
        public int Amount { get; set; }
        public int BookId { get; set; }
    }
    public class OrderResult
    {
        public string? BookName { get; set; }
        public string? BookCategory { get; set; }
        public string? AuthorName { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
