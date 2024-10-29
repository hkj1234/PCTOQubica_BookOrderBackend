using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Core.Book.Entities;

namespace FinalProject.Controllers.Book.Entities
{
    public class BookResultRequest
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string BookCategory { get; set; }
    }

    public class BookResultWithoutIdRequest
    {
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string BookCategory { get; set; }
        public BookResultWithoutId ToBookResultWithoutId() {
            return new BookResultWithoutId
            {
                Title = Title,
                AuthorName = AuthorName,
                BookCategory = BookCategory
            };
        }
    }

    public class BookToUpdateRequest
    {
        public required string Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public BookToUpdate ToBookToUpdate()
        {
            return new BookToUpdate
            {
                Title = Title,
                AuthorId = AuthorId,
                CategoryId = CategoryId
            };
        }
    }
}
