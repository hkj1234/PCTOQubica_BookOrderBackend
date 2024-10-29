using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using FinalProject.Database.Customer.Entities;

namespace FinalProject.Core.Customer.Entities
{
    public class CustomerLogin
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public CustomerLogin() { }
        public CustomerLogin(CustomerRegister reg)
        {
            Email = reg.Email;
            Password = reg.Password;
        }
    }

    public class CustomerRegister
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? VerifyPassword { get; set; }
    }
}