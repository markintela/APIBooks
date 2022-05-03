using Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            var id = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            string er = string.Empty;
            return new ErrorResponse(id, er);

        }
    }
}
