using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Database.Context
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration _configuration;
        public DbContextFactory(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<ApplicationDbContext> CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder
                .UseSqlServer(connectionString);

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}
