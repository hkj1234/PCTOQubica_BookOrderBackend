namespace FinalProject.Core.Book.Exceptions
{
    public class AuthorDoesntExistException : Exception
    {
        public AuthorDoesntExistException() { }
        public AuthorDoesntExistException(string message) : base(message) { }
    }
}
