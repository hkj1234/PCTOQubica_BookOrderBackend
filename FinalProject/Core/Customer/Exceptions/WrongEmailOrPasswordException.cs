namespace FinalProject.Core.Customer.Exceptions
{
    public class WrongEmailOrPasswordException : Exception
    {
        public WrongEmailOrPasswordException()
        {
        }

        public WrongEmailOrPasswordException(string? message) : base(message)
        {
        }
    }
}
