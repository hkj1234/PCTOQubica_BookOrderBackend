using FinalProject.Core.Book.Exceptions;
using FinalProject.Core.BookCategory.Interfaces;
using FinalProject.Core.Customer.Exceptions;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Core.Order.Entities;
using FinalProject.Core.Order.Exeptions;
using FinalProject.Core.Order.Interfaces;
using FinalProject.Database.Order;
using FinalProject.Database.Order.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Core.Order
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBookCategoriesRepository _bookCategoriesRepository;
        private readonly ICustomersRepository _customersRepository;
        public OrderManager(IOrdersRepository ordersRepository, IBookCategoriesRepository bookCategoriesRepository, ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _bookCategoriesRepository = bookCategoriesRepository;
            _customersRepository = customersRepository;
        }
        public async Task<IEnumerable<OrderResult>> GetPesonalOrdersAsync(string? customerEmail)
        {
            string existingCustomerEmail = await CheckCustomerEmail(customerEmail);
            return await _ordersRepository.GetPesonalOrdersAsync(existingCustomerEmail);
        }
        public async Task PostOrderAsync(string? customerEmail, OrderToCreate orderToCreate)
        {
            string existingCustomerEmail = await CheckCustomerEmail(customerEmail);
            if (orderToCreate.Amount <= 0)
                throw new NegativeOrNullAmountException();
            if (! await _bookCategoriesRepository.ExistBookCategories(orderToCreate.BookId))
                throw new BookCategorieDoesntExistException();
            await _ordersRepository.PostOrderAsync(existingCustomerEmail, orderToCreate);
        }

        private async Task<string> CheckCustomerEmail(string? customerEmail)
        {
            if (string.IsNullOrEmpty(customerEmail) || ! await _customersRepository.ExistCustomer(customerEmail))
                throw new CustomerDorsntExistException();
            return customerEmail;
        }
    }
}
