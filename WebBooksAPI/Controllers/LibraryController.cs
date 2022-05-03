using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Library;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin, Manager, Employer")]
        public async Task<IActionResult> Get()
        {
            
                var libraries = await _libraryManager.GetAllLibrariesAsync();
          
                if(libraries == null) {
                    throw new KeyNotFoundException("Library list is empty");
                }
                _logger.LogInformation("Data return success::: {@libraries}", libraries);
                return Ok(libraries);                    
         
        }

        [Authorize(Roles = "Admin, Manager, Employer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
                var library = await _libraryManager.GetLibraryAsync(id);       
                if (library == null) {                                  
                    throw new KeyNotFoundException("Library not found!");                    
                }
                _logger.LogInformation("Data return success::: => {@library}", library);
                return Ok(library);       
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(NewLibrary newLibrary)
        {
            var libraryToCreate = await _libraryManager.CreateLibraryAsync(newLibrary);
            if (libraryToCreate == null)
            {
                throw new ApplicationException("Library not created!");              
            }
            _logger.LogInformation("Data created success => {@libraryToCreate}", libraryToCreate);
            return CreatedAtAction(nameof(Get), new { id = libraryToCreate.Id}, libraryToCreate);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(AlterLibrary library)
        {
            var libraryToUpdate = await _libraryManager.UpdateLibraryAsync(library);

            if(libraryToUpdate == null)
            {
                throw new KeyNotFoundException("Library not found!");
            }
            _logger.LogInformation("Data Updated => {@libraryToUpdate}", libraryToUpdate);
            return Ok(libraryToUpdate);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Library deleted!");
            await _libraryManager.DeleteLibraryAsync(id);
            return NotFound();           
        }
    }
}
