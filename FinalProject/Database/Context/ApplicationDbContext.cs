using Microsoft.EntityFrameworkCore;
using FinalProject.Database.Book.Entities;
using FinalProject.Database.Order.Entities;
using FinalProject.Database.Customer.Entities;
using FinalProject.Database.Author.Entities;
using FinalProject.Database.BookCategory.Entities;
using FinalProject.Database.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DBCustomer> Customers { get; set; }
    public DbSet<DBBook> Books { get; set; }
    public DbSet<DBAuthor> Authors { get; set; }
    public DbSet<DBBookCategory> BookCategories { get; set; }
    public DbSet<DBOrder> Orders { get; set; }
}