using FinalProject.Core.Customer;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Database.Customer;

namespace FinalProject.Controllers.Customer
{
    internal static class CustomerSetup
    {
        public static IServiceCollection AddCustomer(this IServiceCollection services)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            return services;
        }
    }
}
