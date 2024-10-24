using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProject.Core.Customer.Entities;
using FinalProject.Core.Customer.Exceptions;
using FinalProject.Core.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Core.Customer
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomersRepository _customersRepository;
        public CustomerManager(IConfiguration configuration, ICustomersRepository customersRepository)
        {
            _configuration = configuration;
            _customersRepository = customersRepository;
        }
        public async Task RegisterAsync(CustomerRegister model)
        {
            if (model.Password == model.VerifyPassword)
            {
                throw new WrongEmailOrPasswordException();
            }
            if (await _customersRepository.ExistCustomer(model.Email))
            {
                throw new ExisitingEmailException();
            }

            await _customersRepository.AddNewCustomer(model);
        }
        public async Task<string> LoginAsync(CustomerLogin model)
        {
            var customer = await _customersRepository.FirstOrDefaultCustomer(model.Email);
            if (customer == null || customer.Password != model.Password)
            {
                throw new WrongEmailOrPasswordException();
            }

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

            return tokenHandler.WriteToken(token);
        }
    }
}
