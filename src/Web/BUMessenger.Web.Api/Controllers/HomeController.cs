using Microsoft.AspNetCore.Mvc;

namespace BUMessenger.Web.Api.Controllers;

public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
}