namespace FinalProject.Database.Context
{
    public interface IDbContextFactory
    {
        public Task<ApplicationDbContext> CreateDbContext();
    }
}
