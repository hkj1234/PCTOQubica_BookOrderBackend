using FinalProject.Core.Book.Exceptions;
using FinalProject.Core.BookCategory.Interfaces;
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
        public OrderManager(IOrdersRepository ordersRepository, IBookCategoriesRepository bookCategoriesRepository)
        {
            _ordersRepository = ordersRepository;
            _bookCategoriesRepository = bookCategoriesRepository;
        }
        public async Task<IEnumerable<OrderResult>> GetPesonalOrdersAsync(string customerEmail)
        {
            return await _ordersRepository.GetPesonalOrdersAsync(customerEmail);
        }
        public async Task PostOrderAsync(string customerEmail, OrderToCreate orderToCreate)
        {
            if (orderToCreate.Amount <= 0)
                throw new NegativeOrNullAmountException();
            if (! await _bookCategoriesRepository.ExistBookCategories(orderToCreate.BookId))
                throw new BookCategorieDoesntExistException();
            await _ordersRepository.PostOrderAsync(customerEmail, orderToCreate);
        }
    }
}
