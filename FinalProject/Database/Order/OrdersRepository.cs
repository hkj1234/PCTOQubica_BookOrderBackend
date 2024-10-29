using FinalProject.Core.Order.Entities;
using FinalProject.Core.Order.Interfaces;
using FinalProject.Database.Context;
using FinalProject.Database.Customer.Entities;
using FinalProject.Database.Order.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Database.Order
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public OrdersRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<OrderResult>> GetPesonalOrdersAsync(string customerEmail)
        {
            await using var _context = _dbContextFactory.CreateDbContext();

            var ris = await _context.Orders
                .Where(x => x.CustomerEmail == customerEmail)
                .Select(x => new OrderResult
                {
                    BookName = x.Book.Title,
                    BookCategory = x.Book.Author.AuthorName,
                    AuthorName = x.Book.BookCategory.CategoryName,
                    Amount = x.Amount,
                    OrderDateTime = x.OrderDateTime,
                })
                .ToListAsync();

            return ris;
        }
        public async Task PostOrderAsync(string customerEmail, OrderToCreate orderToCreate)
        {
            await using var _context = _dbContextFactory.CreateDbContext();
            DBOrder ris = new DBOrder
            {
                Amount = orderToCreate.Amount,
                CustomerEmail = customerEmail,
                BookId = orderToCreate.BookId,
                OrderDateTime = DateTime.UtcNow,
            };
            _context.Orders.Add(ris);
            _context.SaveChanges();
        }
    }
}
