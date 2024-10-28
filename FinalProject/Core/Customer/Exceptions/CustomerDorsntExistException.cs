namespace FinalProject.Core.Customer.Exceptions
{
    public class CustomerDorsntExistException : Exception
    {
        public CustomerDorsntExistException()
        {
        }

        public CustomerDorsntExistException(string message) : base(message)
        {
        }
    }
}
