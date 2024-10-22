using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(BookResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var ris = await _context.Books
                    .Include(p => p.BookCategory)
                    .Include(p => p.Author)
                    .Select(p => new BookResult
                    {
                        Id = p.Id,
                        Title = p.Title,
                        AuthorName = p.Author.AuthorName,
                        BookCategory = p.BookCategory.CategoryName
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
        [HttpPost("Filter")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(BookResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetWithFilterAsync([FromBody] BookWithoutId b)
        {
            try
            {
                var ris = await _context.Books
                    .Include(p => p.BookCategory)
                    .Include(p => p.Author)
                    .Select(p => new BookResult
                    {
                        Id = p.Id,
                        Title = p.Title,
                        AuthorName = p.Author.AuthorName,
                        BookCategory = p.BookCategory.CategoryName
                    })
                    .Where(p => b.AuthorName == "" || p.AuthorName.Contains(b.AuthorName))
                    .Where(p => b.Title == "" || p.Title.Contains(b.Title))
                    .Where(p => b.BookCategory == "" || p.BookCategory.Contains(b.BookCategory))
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
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Book))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostAsync([FromBody] BookToUpdate b)
        {
            try
            {
                //veidmao se b.CategoryId e b.AuthorId esistono
                if (!(await _context.BookCategories.AnyAsync(x => x.Id == b.CategoryId) &&
                    await _context.Authors.AnyAsync(x => x.Id == b.AuthorId)))
                {
                    return BadRequest();
                }

                //generazione di nuovo book
                Book newBook = new Book
                {
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    CategoryId = b.CategoryId,
                };

                _context.Books.Add(newBook);
                await _context.SaveChangesAsync();

                return Ok(newBook);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}