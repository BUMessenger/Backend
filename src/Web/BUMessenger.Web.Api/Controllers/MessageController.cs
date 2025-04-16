using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Web.Api.Authentification;
using BUMessenger.Web.Dto.Converters;
using BUMessenger.Web.Dto.Models;
using BUMessenger.Web.Dto.Models.Messages;
using Microsoft.AspNetCore.Mvc;

namespace BUMessenger.Web.Api.Controllers;

[ApiController]
[Route("/api/v1/chats/{chatId}/messages")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    
    public MessageController(IMessageService messageService)
    {
        _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(PagedDto<MessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMessagesByChatIdAsync(Guid chatId, [FromQuery] PageFiltersDto filtersDto)
    {
        var userId = User.GetUserId();
        var filters = filtersDto.ToDomain();
        
        var messages = await _messageService.GetMessagesAsync(chatId, userId, filters);
        
        return StatusCode(StatusCodes.Status200OK, messages.ToDto());
    }
    
    [HttpGet("{parentMessageId}/thread")]
    [ProducesResponseType(typeof(PagedDto<MessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetThreadMessagesByChatIdAsync(Guid chatId,
        Guid parentMessageId,
        [FromQuery] PageFiltersDto filtersDto)
    {
        var userId = User.GetUserId();
        var filters = filtersDto.ToDomain();
        
        var messages = await _messageService.GetThreadMessagesAsync(chatId,
            userId,
            parentMessageId,
            filters);
        
        return StatusCode(StatusCodes.Status200OK, messages.ToDto());
    }
    
}