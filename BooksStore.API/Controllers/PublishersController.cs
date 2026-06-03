using BooksStore.API.Models.DTO;
using BooksStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController(IPublisherService publisherService) : ControllerBase
    {
        [HttpGet("get-all-publishers")]
        public async Task<ActionResult<List<PublisherDTO>>> GetPublishers()
            => Ok(await publisherService.GetAllPublishersAsync());


        [HttpGet("get-publisher-by-id/{id}")]
        public async Task<ActionResult<PublisherDTO>> GetPublisher(int id)
        {
            var publisher = await publisherService.GetPublisherByIdAsync(id);

            if (publisher == null) { return NotFound($"Publisher with ID: {id} not found"); }

            return Ok(publisher);
        }


        [HttpPost("add-publisher")]
        public async Task<ActionResult<PublisherDTO>> AddAPublisher(AddPublisherDTO publisherDTO)
        {
            var addedPublisher = await publisherService.AddPublisherAsync(publisherDTO);

            return CreatedAtAction(nameof(GetPublisher), new { id = addedPublisher.Id }, addedPublisher);
        }

        
        [HttpGet("get-publisher-books-with-authors-by-id/{id}")]
        public async Task<ActionResult<PublisherAuthorsBooksDTO>> GetPublisherAuthorsBooks(int id)
        {
            var publisher = await publisherService.GetPublisherAuthorsBooksAsync(id);

            if (publisher == null) { return NotFound($"Publisher with ID: {id} not found"); }

            return Ok(publisher);
        }


        [HttpDelete("delete-publisher-by-id")]
        public async Task<ActionResult> DeletePublisher(int id)
        {
            var deletedPublisher = await publisherService.DeletePublisherAsync(id);

            if (!deletedPublisher) return NotFound($"Publisher with ID: {id} not found");

            return NoContent();
        }
    }
}
