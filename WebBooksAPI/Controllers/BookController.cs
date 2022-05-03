using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Book;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin, Manager, Employer")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {         
                var books = await _bookManager.GetAllBooksAsync();

                if (books  == null) {
                    throw new KeyNotFoundException("Book list is empty");
                }
                _logger.LogInformation("Data return success::: {@books}", books);
                return Ok(books);
        
        }
        [Authorize(Roles = "Admin, Manager, Employer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
           
                var book = await _bookManager.GetBookAsync(id);
               
                if (book == null) {
                    throw new KeyNotFoundException("Book not found!");
                }
                _logger.LogInformation("Data return success::: {@book}", book);
                return Ok(book);          
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(NewBook newBook)
        {
            var bookToCreate = await _bookManager.CreateBookAsync(newBook);
            if (bookToCreate == null) {
                throw new ApplicationException("Book not created!");

            }
            return CreatedAtAction(nameof(Get), new { id = bookToCreate.Id }, bookToCreate);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(AlterBook book)
        {
            var bookToUpdate = await _bookManager.UpdateBookAsync(book);

            if (bookToUpdate == null)
            {
                throw new ApplicationException("Book not updated!");
            }
            return Ok(bookToUpdate);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Book deleted!");
            await _bookManager.DeleteBookAsync(id);
            return NoContent();

        }
    }
}
