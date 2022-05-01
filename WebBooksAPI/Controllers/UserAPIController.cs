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
                return Unauthorized();
            }
            return Ok(userLogged);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                //var userAPI = await _userAPIManager.GetAsync(id);
                //if (userAPI == null) { return NotFound(); }
                //_logger.LogInformation("Success::: {@library}", library);
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                return BadRequest();
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {          
            var userAPI = await _userAPIManager.GetAsync();
            return Ok(userAPI);

        }


        [HttpPost]
        public async Task<ActionResult> Post(NewUserAPI newUserAPI)
        {
            var userCreated = await _userAPIManager.InsertAsync(newUserAPI);

            return CreatedAtAction(nameof(Get), new {login = newUserAPI.Login}, userCreated);

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
