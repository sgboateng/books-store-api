using BooksStore.API.Data;
using BooksStore.API.Models.Domain;
using BooksStore.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.API.Services
{
    public class BookService(ApplicationDbContext context) : IBookService
    {
        public async Task<BookAuthorsDTO> AddBookAsync(AddBookDTO book)
        {
            var newBook = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rating = book.IsRead ? book.Rating : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };

            context.Books.Add(newBook);

            await context.SaveChangesAsync();

            foreach (var id in book.AuthorIds) 
            {
                var newBookAuthor = new Book_Author()
                {
                    BookId = newBook.Id,
                    AuthorId = id
                };

                context.Books_Authors.Add(newBookAuthor);

                await context.SaveChangesAsync();
            }

            var addedBook = await context.Books
                .Where(c => c.Id == newBook.Id)
                .Select(c => new BookAuthorsDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    IsRead = c.IsRead,
                    DateRead = c.DateRead,
                    Rating = c.Rating,
                    Genre = c.Genre,
                    CoverUrl = c.CoverUrl,
                    DateAdded = c.DateAdded,
                    PublisherName = c.Publisher.Name,
                    AuthorNames = c.Book_Authors.Select(c => c.Author.FullName).ToList()
                })
                .FirstOrDefaultAsync();

            return addedBook;
        }

        public async Task<bool> DeleteBookByIdAsync(int id)
        {
            var deleteBook = await context.Books.FindAsync(id);

            if (deleteBook is null) return false;

            context.Books.Remove(deleteBook);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<BookAuthorsDTO>> GetAllBooksAsync()
            => await context.Books.Select(c => new BookAuthorsDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                IsRead = c.IsRead,
                DateRead = c.DateRead,
                Rating = c.Rating,
                Genre = c.Genre,
                CoverUrl = c.CoverUrl,
                DateAdded = c.DateAdded,
                PublisherName = c.Publisher.Name,
                AuthorNames = c.Book_Authors.Select(c => c.Author.FullName).ToList()
            }).ToListAsync();

        public async Task<BookAuthorsDTO?> GetBookByIdAsync(int id)
        {
            var result = await context.Books
                .Where(c => c.Id == id)
                .Select(c => new BookAuthorsDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    IsRead = c.IsRead,
                    DateRead = c.DateRead,
                    Rating = c.Rating,
                    Genre = c.Genre,
                    CoverUrl = c.CoverUrl,
                    DateAdded = c.DateAdded,
                    PublisherName = c.Publisher.Name,
                    AuthorNames = c.Book_Authors.Select(c => c.Author.FullName).ToList()
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> UpdateBookByIdAsync(int id, UpdateBookDTO book)
        {
            var existingBook = await context.Books.FindAsync(id);

            if (existingBook is null) return false;

            existingBook.Description = book.Description;
            existingBook.IsRead = book.IsRead;
            existingBook.DateRead = book.DateRead;
            existingBook.Rating = book.Rating;
            existingBook.Genre = book.Genre;
            existingBook.CoverUrl = book.CoverUrl;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
