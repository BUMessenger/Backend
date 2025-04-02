using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Web.Dto.Converters;
using BUMessenger.Web.Dto.Models;
using BUMessenger.Web.Dto.Models.UnregisteredUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BUMessenger.Web.Api.Controllers;

[ApiController]
[Route("/api/v1/unregistered-users")]
public class UnregisteredUserController : ControllerBase
{
    private readonly IUnregisteredUserService _unregisteredUserService;
    public UnregisteredUserController(IUnregisteredUserService unregisteredUserService)
    {
        _unregisteredUserService = unregisteredUserService ?? throw new ArgumentNullException(nameof(unregisteredUserService));
    }
    
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UnregisteredUserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUnregisteredUserAsync([FromBody] UnregisteredUserCreateDto unregisteredUserCreateDto)
    {
        var unregisteredUserCreateDomain = unregisteredUserCreateDto.ToDomain();
        
        var addedUnregisteredUser = await _unregisteredUserService.AddUnregisteredUserAsync(unregisteredUserCreateDomain);
        
        return StatusCode(StatusCodes.Status201Created, addedUnregisteredUser.ToDto());
    }
}