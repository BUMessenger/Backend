using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models.AuthTokens;
using BUMessenger.Web.Api.Authentification;
using BUMessenger.Web.Api.Authentification.Models;
using BUMessenger.Web.Dto.Models.Auth;
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
    private readonly IAuthTokenService _authTokenService;

    public AuthController(IOptions<JwtSettings> jwtSettings, 
        IUserService userService,
        IAuthTokenService authTokenService)
    {
        _jwtSettings = jwtSettings.Value;
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _authTokenService = authTokenService ?? throw new ArgumentNullException(nameof(authTokenService));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto loginUserDto)
    {
        var user = await _userService.AuthUserByEmailPasswordAsync(loginUserDto.Email, loginUserDto.Password);
        
        var tokens = AuthTokensGenerator.GenerateTokensAsync(user, _jwtSettings);
        var refreshTokenCreate = new AuthTokenCreate
        {
            UserId = user.Id,
            RefreshToken = tokens.RefreshToken,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
        };
        
        await _authTokenService.AddAuthTokenAsync(refreshTokenCreate);

        return StatusCode(StatusCodes.Status200OK, tokens);
    }
}