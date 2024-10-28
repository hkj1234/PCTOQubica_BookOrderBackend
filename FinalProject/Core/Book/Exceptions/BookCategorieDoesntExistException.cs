namespace FinalProject.Core.Book.Exceptions
{
    public class BookCategorieDoesntExistException : Exception
    {
        public BookCategorieDoesntExistException() { }
        public BookCategorieDoesntExistException(string message) : base(message) { }
    }
}
