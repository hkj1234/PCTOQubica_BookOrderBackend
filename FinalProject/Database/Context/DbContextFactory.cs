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

        public ApplicationDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder
                .UseMySql(connectionString, 
                new MySqlServerVersion(new Version(8, 0, 40)));

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}
