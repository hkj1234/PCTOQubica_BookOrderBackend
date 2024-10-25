namespace FinalProject.Core.Order.Exeptions
{
    public class NegativeOrNullAmountException : Exception
    {
        public NegativeOrNullAmountException() { }
        public NegativeOrNullAmountException(string mes) : base(mes) { }
    }
}
