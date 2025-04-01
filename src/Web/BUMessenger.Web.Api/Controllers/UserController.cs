using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models.AuthTokens;
using BUMessenger.Web.Api.Authentification;
using BUMessenger.Web.Api.Authentification.Models;
using BUMessenger.Web.Dto.Converters;
using BUMessenger.Web.Dto.Models;
using BUMessenger.Web.Dto.Models.Auth;
using BUMessenger.Web.Dto.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BUMessenger.Web.Api.Controllers;

[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUserService _userService;
    private readonly IAuthTokenService _authTokenService;

    public UserController(IOptions<JwtSettings> jwtSettings,
        IUserService userService,
        IAuthTokenService authTokenService)
    {
        _jwtSettings = jwtSettings.Value;
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _authTokenService = authTokenService ?? throw new ArgumentNullException(nameof(authTokenService));
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateDto userCreateDto)
    {
        var userCreateDomain = userCreateDto.ToDomain();
        
        var addedUser = await _userService.AddUserAsync(userCreateDomain);
        
        var refreshTokenValue = AuthTokensGenerator.GenerateRefreshToken();
        var refreshTokenCreate = new AuthTokenCreate
        {
            UserId = addedUser.Id,
            RefreshToken = refreshTokenValue,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
        };
        var addedRefreshToken = await _authTokenService.AddAuthTokenAsync(refreshTokenCreate);
        
        var tokens = new AuthResponseDto(AuthTokensGenerator.GenerateJwtToken(addedUser, _jwtSettings, addedRefreshToken.Id),
            addedRefreshToken.RefreshToken);

        return StatusCode(StatusCodes.Status201Created, tokens);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(UsersDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUsersByFiltersAsync(UserFiltersDto filtersDto, PageFiltersDto pageFiltersDto)
    {
        var users = await _userService.GetUsersByFiltersAsync(filtersDto.ToDomain(), pageFiltersDto.ToDomain());
        
        return StatusCode(StatusCodes.Status200OK, users.ToDto());
    }
}