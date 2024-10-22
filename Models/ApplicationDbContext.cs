using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<Order> Orders { get; set; }
}