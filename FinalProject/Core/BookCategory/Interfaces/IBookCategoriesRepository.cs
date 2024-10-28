namespace FinalProject.Core.BookCategory.Interfaces
{
    public interface IBookCategoriesRepository
    {
        public Task<bool> ExistBookCategories(int id);
    }
}
