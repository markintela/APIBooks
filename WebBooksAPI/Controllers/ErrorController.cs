using Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebBooksAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            Response.StatusCode = 500;
            return new ErrorResponse();

        }
    }
}
