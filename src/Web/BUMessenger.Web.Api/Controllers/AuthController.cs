using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Web.Api.Authentification.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BUMessenger.Web.Api.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUserService _userService;

    public AuthController(IOptions<JwtSettings> jwtSettings, IUserService userService)
    {
        _jwtSettings = jwtSettings.Value;
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    
    /*[HttpPost("login")]
    [AllowAnonymous]
    [Produces]*/
}