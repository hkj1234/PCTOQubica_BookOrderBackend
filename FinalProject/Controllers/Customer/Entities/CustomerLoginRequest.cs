using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using FinalProject.Core.Customer.Entities;

namespace FinalProject.Controllers.Customer.Entities
{
    public class CustomerLoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public CustomerLogin ToCoreCustomerLogin()
        {
            return new CustomerLogin()
            {
                Email = Email,
                Password = Password
            };
        }
    }

    public class CustomerRegisterRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string VerifyPassword { get; set; }
        public CustomerRegister ToCoreCustomerRegister()
        {
            return new CustomerRegister()
            {
                Email = Email,
                Password = Password,
                VerifyPassword = VerifyPassword
            };
        }
    }
}