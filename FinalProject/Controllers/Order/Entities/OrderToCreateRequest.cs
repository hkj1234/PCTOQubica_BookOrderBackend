using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Core.Customer.Entities;
using FinalProject.Core.Order.Entities;
using FinalProject.Database.Book.Entities;
using Microsoft.VisualBasic;

namespace FinalProject.Controllers.Order.Entities
{
    public class OrderToCreateRequest
    {
        public int Amount { get; set; }
        public int BookId { get; set; }
        public OrderToCreate ToOrderToCreate()
        {
            return new OrderToCreate { Amount = Amount, BookId = BookId };
        }
    }
    public class OrderResultRequest
    {
        public string BookName { get; set; }
        public string BookCategory { get; set; }
        public string AuthorName { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
