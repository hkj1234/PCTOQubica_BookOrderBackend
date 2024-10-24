using FinalProject.Controllers.Customer.Entities;
using FinalProject.Core.Customer.Entities;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Database.Customer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Database.Customer
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomersRepository(ApplicationDbContext context) 
        {
            _context = context; 
        }
        public async Task<bool> ExistCustomer(string email)
        {
            return await _context.Customers.AnyAsync(x => x.Email == email);
        }
        public async Task<DBCustomer> FirstOrDefaultCustomer(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task AddNewCustomer(CustomerRegister model)
        {
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
