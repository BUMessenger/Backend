using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BUMessenger.Web.Api.Controllers;

[ApiController]
[Route("/api/v1")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [Produces("application/json")]
    public IActionResult GetItemById(int id)
    {
        throw new Exception();
    }
}