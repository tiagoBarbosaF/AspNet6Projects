using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    // [ApiKey]  -> Test using apikey
    public IActionResult Get([FromServices] IConfiguration configuration)
    {
        return Ok(new
        {
            environment = configuration.GetValue<string>("Env"),
            status = "API online"
        });
    }
}