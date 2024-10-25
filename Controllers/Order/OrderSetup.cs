
using FinalProject.Core.Order;
using FinalProject.Core.Order.Interfaces;
using FinalProject.Database.Order;

namespace FinalProject.Controllers.Order
{
    public static class OrderSetup
    {
        public static IServiceCollection AddOrder(this IServiceCollection services)
        {
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            return services;
        }
    }
}
