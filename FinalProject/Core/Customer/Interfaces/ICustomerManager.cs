using FinalProject.Core.Customer.Entities;

namespace FinalProject.Core.Customer.Interfaces
{
    public interface ICustomerManager
    {
        public Task<string> LoginAsync(CustomerLogin model);
        public Task RegisterAsync(CustomerRegister model);
    }
}
