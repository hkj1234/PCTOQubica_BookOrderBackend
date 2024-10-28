using FinalProject.Core.Author.Interfaces;
using FinalProject.Core.Book.Interfaces;
using FinalProject.Core.Book;
namespace FinalProject.Database.Context
{
    public static class ContextSetup
    {
        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddScoped<IDbContextFactory, DbContextFactory>();
            return services;
        }
    }
}
