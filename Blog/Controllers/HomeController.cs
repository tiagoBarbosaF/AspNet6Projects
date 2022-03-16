using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

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