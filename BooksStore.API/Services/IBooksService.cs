using BooksStore.API.Models.DTO;

namespace BooksStore.API.Services
{
    public interface IBooksService
    {
        Task<List<BooksDTO>> GetAllBooksAsync();

        Task<BooksDTO?> GetBookByIdAsync(int id);

        Task<BooksDTO> AddBookAsync(AddBookDTO book);

        Task<bool> UpdateBookByIdAsync(int id, UpdateBookDTO book);

        Task<bool> DeleteBookByIdAsync(int id);
    }
}
