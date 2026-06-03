using BooksStore.API.Models.DTO;
using BooksStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookService booksService) : ControllerBase
    {
        [HttpGet("get-all-books")]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
            => Ok(await booksService.GetAllBooksAsync());


        [HttpGet("get-book-by-id/{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await booksService.GetBookByIdAsync(id);

            if (book == null) { return NotFound($"Book with ID: {id} not found"); }

            return Ok(book);
        }

        
        [HttpPost("add-book-with-authors")]
        public async Task<ActionResult<BookDTO>> AddBook(AddBookDTO bookDTO)
        {
            var addedBook = await booksService.AddBookAsync(bookDTO);

            return CreatedAtAction(nameof(GetBook), new { id = addedBook.Id }, addedBook);
        }

        
        [HttpPut("update-book-by-id/{id}")]
        public async Task<ActionResult> UpdateBook(int id, UpdateBookDTO bookDTO)
        {
            var updatedBook = await booksService.UpdateBookByIdAsync(id, bookDTO);

            if (!updatedBook) return NotFound($"Book with ID: {id} not found");

            return NoContent();
        }

        
        [HttpDelete("delete-book-by-id/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var deletedBook = await booksService.DeleteBookByIdAsync(id);

            if (!deletedBook) return NotFound($"Book with ID: {id} not found");

            return NoContent();
        }
    }
}
