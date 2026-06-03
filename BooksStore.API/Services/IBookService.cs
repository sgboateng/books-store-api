using BooksStore.API.Models.DTO;

namespace BooksStore.API.Services
{
    public interface IBookService
    {
        Task<List<BookAuthorsDTO>> GetAllBooksAsync();

        Task<BookAuthorsDTO?> GetBookByIdAsync(int id);

        Task<BookAuthorsDTO> AddBookAsync(AddBookDTO book);

        Task<bool> UpdateBookByIdAsync(int id, UpdateBookDTO book);

        Task<bool> DeleteBookByIdAsync(int id);
    }
}
