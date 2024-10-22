using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace FinalProject.Models
{
    public class Customer
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public Customer() { }
        public Customer(CustomerRegister reg) 
        {
            Email = reg.Email;
            Password = reg.Password;
        }
    }

    public class CustomerRegister
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string VerifyPassword { get; set; }
    }
}