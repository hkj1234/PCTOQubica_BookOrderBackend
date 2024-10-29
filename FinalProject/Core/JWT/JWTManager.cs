using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Core.JWT.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Core.JWT
{
    public class JWTManager : IJWTManager
    {
        private readonly IConfiguration _configuration;
        public JWTManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string? JWTGenerate(string? data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
#pragma warning disable 8602, 8604
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
                new Claim(ClaimTypes.Name, data)
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

#pragma warning restore 8602, 8604
            return tokenHandler.WriteToken(token);
        }
    }
}
