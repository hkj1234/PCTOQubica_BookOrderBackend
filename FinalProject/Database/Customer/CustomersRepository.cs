using FinalProject.Controllers.Customer.Entities;
using FinalProject.Core.Customer.Entities;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Database.Context;
using FinalProject.Database.Customer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FinalProject.Database.Customer
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public CustomersRepository(IDbContextFactory dbContextFactory) 
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<bool> ExistCustomer(string email)
        {
            await using var _context = _dbContextFactory.CreateDbContext();
            return await _context.Customers.AnyAsync(x => x.Email == email);
        }
        public async Task<DBCustomer> FirstOrDefaultCustomer(string email)
        {
            await using var _context = _dbContextFactory.CreateDbContext();
            DBCustomer? ris = await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
            if (ris == null) 
            {
                ris = new DBCustomer();
            }
            return ris;
        }
        public async Task AddNewCustomer(CustomerRegister model)
        {
            await using var _context = _dbContextFactory.CreateDbContext();
            DBCustomer newCustomer = new DBCustomer
            {
                Email = model.Email,
                Password = model.Password,
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
        }
    }
}
