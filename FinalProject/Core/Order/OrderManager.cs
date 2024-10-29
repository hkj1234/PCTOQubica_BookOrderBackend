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
            await CheckCustomerEmail(customerEmail);
            return await _ordersRepository.GetPesonalOrdersAsync(customerEmail);
        }
        public async Task PostOrderAsync(string? customerEmail, OrderToCreate orderToCreate)
        {
            await CheckCustomerEmail(customerEmail);
            if (orderToCreate.Amount <= 0)
                throw new NegativeOrNullAmountException();
            if (! await _bookCategoriesRepository.ExistBookCategories(orderToCreate.BookId))
                throw new BookCategorieDoesntExistException();
            await _ordersRepository.PostOrderAsync(customerEmail, orderToCreate);
        }

        private async Task CheckCustomerEmail(string? customerEmail)
        {
            if (customerEmail == null || ! await _customersRepository.ExistCustomer(customerEmail))
                throw new CustomerDorsntExistException();
        }
    }
}
