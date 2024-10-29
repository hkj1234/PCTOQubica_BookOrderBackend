namespace FinalProject.Core.Customer.Exceptions
{
    public class ExisitingEmailException : Exception
    {
        public ExisitingEmailException()
        {
        }

        public ExisitingEmailException(string? message) : base(message)
        {
        }
    }
}
