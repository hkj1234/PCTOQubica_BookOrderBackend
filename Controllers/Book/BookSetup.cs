using FinalProject.Core.Author.Interfaces;
using FinalProject.Core.Book;
using FinalProject.Core.Book.Interfaces;
using FinalProject.Core.BookCategory.Interfaces;
using FinalProject.Database.Author;
using FinalProject.Database.Book;
using FinalProject.Database.BookCategory;

namespace FinalProject.Controllers.Book
{
    public static class BookSetup
    {
        public static IServiceCollection AddBook(this IServiceCollection services)
        {
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            services.AddScoped<IBookCategoriesRepository, BookCategoriesRepository>();
            return services;
        }
    }
}
