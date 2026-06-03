using BooksStore.API.Data;
using BooksStore.API.Models.Domain;
using BooksStore.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.API.Services
{
    public class PublisherService(ApplicationDbContext context) : IPublisherService
    {
        public async Task<PublisherDTO> AddPublisherAsync(AddPublisherDTO publisher)
        {
            var newPublisher = new Publisher()
            {
                Name = publisher.Name,
            };

            context.Publishers.Add(newPublisher);

            await context.SaveChangesAsync();

            return new PublisherDTO()
            {
                Id = newPublisher.Id,
                Name = newPublisher.Name
            };
        }

        public async Task<bool> DeletePublisherAsync(int id)
        {
            var deletePublisher = await context.Publishers.FindAsync(id);

            if (deletePublisher is null) return false;

            context.Publishers.Remove(deletePublisher);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<PublisherDTO>> GetAllPublishersAsync()
            => await context.Publishers.Select(c => new PublisherDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();

        public async Task<PublisherAuthorsBooksDTO?> GetPublisherAuthorsBooksAsync(int id)
        {
            var result = await context.Publishers
                .Where(c => c.Id == id)
                .Select(c => new PublisherAuthorsBooksDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    BooksAuthors = c.Books.Select(c => new BooksAuthorsDTO()
                    {
                        BookName = c.Title,
                        BooksAuthors = c.Book_Authors.Select(c => c.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<PublisherDTO?> GetPublisherByIdAsync(int id)
        {
            var result = await context.Publishers
                .Where(c => c.Id == id)
                .Select(c => new PublisherDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).FirstOrDefaultAsync();

            return result;
        }
    }
}
