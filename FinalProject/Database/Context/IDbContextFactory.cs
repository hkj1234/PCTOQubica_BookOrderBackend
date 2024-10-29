namespace FinalProject.Database.Context
{
    public interface IDbContextFactory
    {
        public ApplicationDbContext CreateDbContext();
    }
}
