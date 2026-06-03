using BooksStore.API.Models.DTO;

namespace BooksStore.API.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorDTO>> GetAllAuthorsAsync();

        Task<AuthorDTO?> GetAuthorByIdAsync(int id);

        Task<AuthorBooksDTO?> GetAuthorBooksByIdAsync(int id);

        Task<AuthorDTO> AddAuthorAsync(AddAuthorDTO author);
    }
}
