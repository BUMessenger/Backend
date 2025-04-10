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
        
        var refreshTokenValue = AuthTokensGenerator.GenerateRefreshToken();
        var refreshTokenCreate = new AuthTokenCreate
        {
            UserId = user.Id,
            RefreshToken = refreshTokenValue,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
        };
        var addedRefreshToken = await _authTokenService.AddAuthTokenAsync(refreshTokenCreate);
        
        var tokens = new AuthResponseDto(AuthTokensGenerator.GenerateJwtToken(user, _jwtSettings, addedRefreshToken.Id),
            addedRefreshToken.RefreshToken);

        return StatusCode(StatusCodes.Status200OK, tokens);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenDto refreshTokenDto)
    {
        var refreshToken = await _authTokenService.GetAuthTokenByRefreshTokenAsync(refreshTokenDto.RefreshToken);
        
        var user = await _userService.GetUserByIdAsync(refreshToken.UserId);
        
        var authToken = AuthTokensGenerator.GenerateJwtToken(user, _jwtSettings, refreshToken.Id);
        
        var tokens = new AuthResponseDto(authToken, refreshToken.RefreshToken);
        
        return StatusCode(StatusCodes.Status200OK, tokens);
    }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LogoutAsync([FromBody] RefreshTokenDto refreshTokenDto)
    {
        var refreshToken = refreshTokenDto.RefreshToken;
        
        await _authTokenService.RevokeRefreshTokenByRefreshTokenAsync(refreshToken);
        
        return StatusCode(StatusCodes.Status200OK);
    }
}