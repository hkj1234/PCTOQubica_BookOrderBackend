using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("")]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public CustomersController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        //Login
        [HttpPost("Login")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> LoginAsync(Customer model)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (customer.Result != null && customer.Result.Password == model.Password)
                {
                    //legge la configurazione di TokenOptions
                    var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
                    //prende sicret
                    var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        //configurazione del JWT:
                        //utente
                        Subject = new ClaimsIdentity(new[]
                        {
                    new Claim(ClaimTypes.Name, model.Email)
                    }),
                        //Issuer: colui che ha creato il token
                        Issuer = tokenOptions.Issuer,
                        //Audience: chi utilizzera questo token, cioè quali sono server e API 
                        Audience = tokenOptions.Audience,
                        //scadenza
                        Expires = DateTime.UtcNow.AddDays(tokenOptions.ExpiryDays),
                        //algoritmo di generazione della firma, serve per controllare che la token hai creato tu
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    //seguente codice è per creare token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new { token = tokenHandler.WriteToken(token) });
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }


        //Register
        [HttpPost("Register")]
        [SwaggerResponse(StatusCodes.Status200OK, null, null)]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> RegisterAsync(CustomerRegister model)
        {
            try
            {
                if (model.Password == model.VerifyPassword && await _context.Customers.FindAsync(model.Email) == null)
                {
                    Customer newUser = new Customer(model);
                    _context.Customers.Add(newUser);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}
