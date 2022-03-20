using Microsoft.AspNetCore.Mvc;

namespace BlogDotnet5.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        // [ApiKey]  -> Test using apikey
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "API online"
            });
        }
    }
}