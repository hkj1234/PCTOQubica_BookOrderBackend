using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using FinalProject.Core.Order.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Controllers.Order
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OrderResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostAsync()
        {
            try
            {
                string customerEmail = HttpContext.User.Identity.Name;
                var ris = await _context.Orders
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.Book)
                    .ThenInclude(x => x.BookCategory)
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
                return Ok(ris);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }


        [Authorize]
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status200OK, null, null)]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetWithFilterAsync([FromBody] OrderToCreate orderToCreate)
        {
            try
            {
                string customerEmail = HttpContext.User.Identity.Name;
                OrderDB ris = new OrderDB
                {
                    Amount = orderToCreate.Amount,
                    CustomerEmail = customerEmail,
                    BookId = orderToCreate.BookId,
                    OrderDateTime = DateTime.UtcNow,
                };
                _context.Orders.Add(ris);
                _context.SaveChanges();
                return Ok(ris);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}