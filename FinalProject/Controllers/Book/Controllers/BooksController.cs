using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProject.Controllers.Book.Entities;
using FinalProject.Core.Book.Entities;
using FinalProject.Core.Book.Exceptions;
using FinalProject.Core.Book.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Controllers.Book.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookManager _bookManager;
        public BooksController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }


        [Authorize]
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(BookResultRequest))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetAsync()
        {
            var ris = await _bookManager.GetBooksAsync();
            return Ok(ris);
        }


        [Authorize]
        [HttpPost("Filter")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(BookResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetWithFilterAsync([FromBody] BookResultWithoutIdRequest b)
        {
            try
            {
                var ris = await _bookManager.GetBooksWithFilterAsync(b.ToBookResultWithoutId());
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
        public async Task<IActionResult> PostAsync([FromBody] BookToUpdateRequest b)
        {
            try
            {
                await _bookManager.PostBookAsync(b.ToBookToUpdate());

                return Ok();
            }
            catch (BookCategorieDoesntExistException e)
            {
                return BadRequest($"BookCategorie does'nt exist {e.Message}");
            }
            catch (AuthorDoesntExistException e)
            {
                return BadRequest($"Author does'nt exist {e.Message}");
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}