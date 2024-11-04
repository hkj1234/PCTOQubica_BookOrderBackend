namespace FinalProject.Core.Order.Exceptions
{
    public class NegativeOrNullAmountException : Exception
    {
        public NegativeOrNullAmountException() { }
        public NegativeOrNullAmountException(string? mes) : base(mes) { }
    }
}
