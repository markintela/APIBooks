using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Library;
using Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {

        private readonly ILibraryManager _libraryManager;
        private readonly ILogger _logger;

        public LibraryController(ILibraryManager libraryManager, ILogger<LibraryController> logger)
        {
            _libraryManager = libraryManager;
            _logger = logger;
        }
       
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var libraries = await _libraryManager.GetAllLibrariesAsync();
          
                if(libraries == null) { return NotFound(); }
                _logger.LogInformation("Success::: {@libraries}", libraries);
                return Ok(libraries);
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
                var library = await _libraryManager.GetLibraryAsync(id);
                var teste = Response.Body;
                if (library == null) { return NotFound(); }
                _logger.LogInformation("Success::: {@library}", library);
                return Ok(library);
            }
            catch (Exception ex) {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
            
        }

  
        [HttpPost]
        public async Task<IActionResult> Post(NewLibrary newLibrary)
        {
            var libraryToCreate = await _libraryManager.CreateLibraryAsync(newLibrary);
            return CreatedAtAction(nameof(Get), new { id = libraryToCreate.Id}, libraryToCreate);
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(AlterLibrary library)
        {
            var libraryToUpdate = await _libraryManager.UpdateLibraryAsync(library);

            if(libraryToUpdate == null)
            {
                return NotFound();
            }
            return Ok(libraryToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _libraryManager.DeleteLibraryAsync(id);
            return NoContent();
            
        }
    }
}
