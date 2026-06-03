using BooksStore.API.Models.DTO;
using BooksStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(IAuthorService authorService) : ControllerBase
    {
        [HttpGet("get-all-authors")]
        public async Task<ActionResult<List<AuthorDTO>>> GetAuthors()
    => Ok(await authorService.GetAllAuthorsAsync());


        [HttpGet("get-author-by-id/{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var author = await authorService.GetAuthorByIdAsync(id);

            if (author == null) { return NotFound($"Author with ID: {id} not found"); }

            return Ok(author);
        }

        
        [HttpGet("get-author-books-by-id/{id}")]
        public async Task<ActionResult<AuthorBooksDTO>> GetAuthorBooks(int id)
        {
            var author = await authorService.GetAuthorBooksByIdAsync(id);

            if (author == null) { return NotFound($"Author with ID: {id} not found"); }

            return Ok(author);
        }


        [HttpPost("add-author")]
        public async Task<ActionResult<AuthorDTO>> AddAuthor(AddAuthorDTO authorDTO)
        {
            var addedAuthor = await authorService.AddAuthorAsync(authorDTO);

            return CreatedAtAction(nameof(GetAuthor), new { id = addedAuthor.Id }, addedAuthor);
        }
    }
}
