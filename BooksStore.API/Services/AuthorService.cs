using BooksStore.API.Data;
using BooksStore.API.Models.Domain;
using BooksStore.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.API.Services
{
    public class AuthorService(ApplicationDbContext context) : IAuthorService
    {
        public async Task<AuthorDTO> AddAuthorAsync(AddAuthorDTO author)
        {
            var newAuthor = new Author()
            {
                FullName = author.FullName,
            };

            context.Authors.Add(newAuthor);

            await context.SaveChangesAsync();

            return new AuthorDTO()
            {
                Id = newAuthor.Id,
                FullName = newAuthor.FullName
            };
        }

        public async Task<List<AuthorDTO>> GetAllAuthorsAsync()
            => await context.Authors.Select(c => new AuthorDTO
            {
                Id = c.Id,
                FullName = c.FullName
            }).ToListAsync();

        public async Task<AuthorBooksDTO?> GetAuthorBooksByIdAsync(int id)
        {
            var result = await context.Authors.Where(c => c.Id == id)
                .Select(c => new AuthorBooksDTO
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    BookTitles = c.Book_Authors.Select(c => c.Book.Title).ToList()
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<AuthorDTO?> GetAuthorByIdAsync(int id)
        {
            var result = await context.Authors
                .Where(c => c.Id == id)
                .Select(c => new AuthorDTO
                {
                    Id = c.Id,
                    FullName = c.FullName
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
