using BooksStore.API.Data;
using BooksStore.API.Models.Domain;
using BooksStore.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.API.Services
{
    public class BooksService(ApplicationDbContext context) : IBooksService
    {
        public async Task<BooksDTO> AddBookAsync(AddBookDTO book)
        {
            var newBook = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rating = book.IsRead ? book.Rating : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };

            context.Books.Add(newBook);

            await context.SaveChangesAsync();

            return new BooksDTO()
            {
                Id = newBook.Id,
                Title = newBook.Title,
                Description = newBook.Description,
                IsRead = newBook.IsRead,
                DateRead = newBook.DateRead,
                Rating = newBook.Rating,
                Genre = newBook.Genre,
                Author = newBook.Author,
                CoverUrl = newBook.CoverUrl,
                DateAdded = newBook.DateAdded
            };
        }

        public async Task<bool> DeleteBookByIdAsync(int id)
        {
            var deleteBook = await context.Books.FindAsync(id);

            if (deleteBook is null) return false;

            context.Books.Remove(deleteBook);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<BooksDTO>> GetAllBooksAsync()
            => await context.Books.Select(c => new BooksDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                IsRead = c.IsRead,
                DateRead = c.DateRead,
                Rating = c.Rating,
                Genre = c.Genre,
                Author = c.Author,
                CoverUrl = c.CoverUrl,
                DateAdded = c.DateAdded
            }).ToListAsync();

        public async Task<BooksDTO?> GetBookByIdAsync(int id)
        {
            var result = await context.Books
                .Where(c => c.Id == id)
                .Select(c => new BooksDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    IsRead = c.IsRead,
                    DateRead = c.DateRead,
                    Rating = c.Rating,
                    Genre = c.Genre,
                    Author = c.Author,
                    CoverUrl = c.CoverUrl,
                    DateAdded = c.DateAdded
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
            existingBook.Author = book.Author;
            existingBook.CoverUrl = book.CoverUrl;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
