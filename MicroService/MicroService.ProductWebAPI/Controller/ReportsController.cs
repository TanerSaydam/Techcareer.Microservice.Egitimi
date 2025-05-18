using Microsoft.AspNetCore.Mvc;

namespace MicroService.ProductWebAPI.Controller;
[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    [HttpGet]
    [MyAuthorize("reports")]
    public IActionResult Get()
    {
        return Ok();
    }
}
