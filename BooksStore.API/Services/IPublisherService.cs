using BooksStore.API.Models.DTO;

namespace BooksStore.API.Services
{
    public interface IPublisherService
    {
        Task<List<PublisherDTO>> GetAllPublishersAsync();

        Task<PublisherDTO?> GetPublisherByIdAsync(int id);

        Task<PublisherAuthorsBooksDTO?> GetPublisherAuthorsBooksAsync(int id);

        Task<PublisherDTO> AddPublisherAsync(AddPublisherDTO publisher);

        Task<bool> DeletePublisherAsync(int id);
    }
}
