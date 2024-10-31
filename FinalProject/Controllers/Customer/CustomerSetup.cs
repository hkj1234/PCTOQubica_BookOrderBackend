using FinalProject.Core.Customer;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Core.JWT;
using FinalProject.Core.JWT.Interfaces;
using FinalProject.Database.Customer;

namespace FinalProject.Controllers.Customer
{
    internal static class CustomerSetup
    {
        public static IServiceCollection AddCustomer(this IServiceCollection services)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IJWTManager, JWTManager>();
            services.AddScoped<IGetOptionManager, GetOptionManager>();
            return services;
        }
    }
}

