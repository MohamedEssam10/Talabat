using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    [Route("api/Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Error(int code) 
        {
            if (code == 401)
                return Unauthorized(new APIResponse(401));
            else if (code == 404)
                return NotFound(new APIResponse(404));
            else
                return StatusCode(code); 
        }
    }
}
