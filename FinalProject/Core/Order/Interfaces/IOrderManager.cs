using FinalProject.Core.Order.Entities;

namespace FinalProject.Core.Order.Interfaces
{
    public interface IOrderManager
    {
        public Task<IEnumerable<OrderResult>> GetPesonalOrdersAsync(string? customerEmail);
        public Task PostOrderAsync(string? customerEmail, OrderToCreate orderToCreate);
    }
}
