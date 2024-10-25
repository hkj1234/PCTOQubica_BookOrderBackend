namespace FinalProject.Core.Author.Interfaces
{
    public interface IAuthorsRepository
    {
        public Task<bool> ExistAuthors(int id);
    }
}
