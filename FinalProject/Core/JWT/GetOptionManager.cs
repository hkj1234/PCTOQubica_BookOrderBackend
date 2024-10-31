using FinalProject.Core.JWT.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FinalProject.Core.JWT
{
    public class GetOptionManager : IGetOptionManager
    {
        private readonly IConfiguration _configuration;
        public GetOptionManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenOptions? GetTokenOptions()
        {
            return _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
    }
}
