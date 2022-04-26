using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Book;
using Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookManager _bookManager;
        private readonly ILogger _logger;

        public BookController(IBookManager libraryManager, ILogger<BookController> logger)
        {
            _bookManager = libraryManager;
            _logger = logger;
        }
        // GET: api/<BookController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var books = await _bookManager.GetAllBooksAsync();

                if (books  == null) { return NotFound(); }
                _logger.LogInformation("Success::: {@books}", books);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var book = await _bookManager.GetBookAsync(id);
               
                if (book == null) { return NotFound(); }
                _logger.LogInformation("Success::: {@book}", book);
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(NewBook newBook)
        {
            var bookToCreate = await _bookManager.CreateBookAsync(newBook);
            return CreatedAtAction(nameof(Get), new { id = bookToCreate.Id }, bookToCreate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(AlterBook book)
        {
            var bookToUpdate = await _bookManager.UpdateBookAsync(book);

            if (bookToUpdate == null)
            {
                return NotFound();
            }
            return Ok(bookToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _bookManager.DeleteBookAsync(id);
            return NoContent();

        }
    }
}
