using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using FinalProject.Controllers.Order.Entities;
using FinalProject.Core.Book.Exceptions;
using FinalProject.Core.Customer.Exceptions;
using FinalProject.Core.Order.Entities;
using FinalProject.Core.Order.Exeptions;
using FinalProject.Core.Order.Interfaces;
using FinalProject.Database.Order.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Controllers.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }


        [Authorize]
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(OrderResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetPesonalOrdersAsync()
        {
            string? customerEmail = HttpContext.User.Identity?.Name;
            return Ok(await _orderManager.GetPesonalOrdersAsync(customerEmail));
        }


        [Authorize]
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status200OK, null, null)]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostOrderAsync([FromBody] OrderToCreateRequest orderToCreate)
        {
            try
            {
                string? customerEmail = HttpContext.User.Identity?.Name;
                await _orderManager.PostOrderAsync(customerEmail, orderToCreate.ToOrderToCreate());
                return Ok();
            }
            catch (NegativeOrNullAmountException e)
            {
                return BadRequest($"Amount must greater than 0 {e.Message}");
            }
            catch (BookCategorieDoesntExistException e)
            {
                return BadRequest($"BookCategorie does'nt exist {e.Message}");
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }

        }
    }
}