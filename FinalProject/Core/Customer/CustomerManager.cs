using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProject.Core.Customer.Entities;
using FinalProject.Core.Customer.Exceptions;
using FinalProject.Core.Customer.Interfaces;
using FinalProject.Core.JWT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Core.Customer
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IJWTManager _jWTManager;
        private readonly ICustomersRepository _customersRepository;
        public CustomerManager(IJWTManager jWTManager, ICustomersRepository customersRepository)
        {
            _jWTManager = jWTManager;
            _customersRepository = customersRepository;
        }
        public async Task RegisterAsync(CustomerRegister model)
        {
            if (model.Password != model.VerifyPassword)
            {
                throw new WrongEmailOrPasswordException();
            }
            if (await _customersRepository.ExistCustomer(model.Email))
            {
                throw new ExisitingEmailException();
            }

            await _customersRepository.AddNewCustomer(model);
        }
        public async Task<string> LoginAsync(CustomerLogin model)
        {
            var customer = await _customersRepository.FirstOrDefaultCustomer(model.Email);
            if (customer == null || customer.Password != model.Password)
            {
                throw new WrongEmailOrPasswordException();
            }

            return _jWTManager.JWTGenerate(model.Email);
        }
    }
}
