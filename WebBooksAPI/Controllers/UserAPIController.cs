using Core.Domain;
using Core.Shared.ModelViews.UserAPI;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {

        private readonly IUserAPIManager _userAPIManager;
        private readonly ILogger _logger;


        public UserAPIController(IUserAPIManager userAPIManager, ILogger<UserAPIController> logger)
        {
            _userAPIManager = userAPIManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult> Login( [FromBody] UserAPI userAPI)
        {
            var userLogged = await _userAPIManager.ValidaUserAPIEGeraTokenAsync(userAPI);

            if(userLogged == null)
            {
                throw new ApplicationException("Login not created!");
                return Unauthorized();
            }
            _logger.LogInformation("Login success::: {@userLogged}", userLogged.Login);
            return Ok(userLogged);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {                         
             var userAPI = await _userAPIManager.GetAsync(id);
             if (userAPI == null) {
                throw new ApplicationException("User not found!");
             }
             _logger.LogInformation("Data return success::: {@userLogged}", userAPI);
             return Ok(userAPI);                  
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {          
            var userAPI = await _userAPIManager.GetAsync();
            if (userAPI == null) {
                throw new ApplicationException("The list is empty!");
            }
            return Ok(userAPI);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post(NewUserAPI newUserAPI)
        {
            var userCreated = await _userAPIManager.InsertAsync(newUserAPI);
            if (userCreated == null) {
                throw new ApplicationException("User not created!");
            }
            _logger.LogInformation("Data return success::: {@userCreated}", userCreated.Login);
            return CreatedAtAction(nameof(Get), new {login = newUserAPI.Login}, userCreated);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
