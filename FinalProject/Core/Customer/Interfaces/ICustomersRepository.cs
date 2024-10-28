using FinalProject.Core.Customer.Entities;
using FinalProject.Database.Customer.Entities;

namespace FinalProject.Core.Customer.Interfaces
{
    public interface ICustomersRepository
    {
        public Task<bool> ExistCustomer(string email);
        public Task<DBCustomer> FirstOrDefaultCustomer(string email);
        public Task AddNewCustomer(CustomerRegister model);
    }
}
