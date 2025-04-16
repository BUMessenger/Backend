using System.Security.Claims;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Web.Api.Authentification;
using BUMessenger.Web.Dto.Converters;
using BUMessenger.Web.Dto.Models;
using BUMessenger.Web.Dto.Models.Chats;
using BUMessenger.Web.Dto.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BUMessenger.Web.Api.Controllers;

[ApiController]
[Route("/api/v1/chats")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
    }
    
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(PagedDto<ChatSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetChatsByUserIdAsync([FromQuery] PageFiltersDto filters)
    {
        var userId = User.GetUserId();
        var filtersDomain = filters.ToDomain();
        
        var chats = await _chatService.GetChatsByUserIdAsync(userId, filtersDomain);
        
        return StatusCode(StatusCodes.Status200OK, chats.ToDto());
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ChatDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateChatAsync([FromBody] ChatCreateDto chatCreateDto)
    {
        var userId = User.GetUserId();
        var chatCreate = chatCreateDto.ToDomain();
        
        var addedChat = await _chatService.CreateChatAsync(userId, chatCreate);
        
        return StatusCode(StatusCodes.Status201Created, addedChat.ToDto());
    }
    
    [HttpGet("{chatId}")]
    [Authorize]
    [ProducesResponseType(typeof(ChatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetChatByIdAsync(Guid chatId)
    {
        var userId = User.GetUserId();
        
        var chat = await _chatService.GetChatByIdAsync(chatId, userId);
        
        return StatusCode(StatusCodes.Status200OK, chat.ToDto());
    }
    
    [HttpDelete("{chatId}/leave")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LeaveChat(Guid chatId)
    {
        var userId = User.GetUserId();
        await _chatService.RemoveUserFromChatAsync(chatId, userId);
        
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpPatch("{chatId}/name")]
    [Authorize]
    [ProducesResponseType(typeof(ChatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateChatNameAsync(Guid chatId,
        [FromBody] ChatNameUpdateDto chatNameUpdateDto)
    {
        var userId = User.GetUserId();
        var chatNameUpdate = chatNameUpdateDto.ToDomain();
        
        var updatedChat = await _chatService.UpdateChatNameAsync(chatId, userId, chatNameUpdate);
        
        return StatusCode(StatusCodes.Status200OK, updatedChat.ToDto());
    }
    
    [HttpPost("{chatId}/users")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddUserToChatAsync(Guid chatId, [FromBody] Guid userAddId)
    {
        var userId = User.GetUserId();
        
        await _chatService.AddUserToChatAsync(chatId, userId, userAddId);
        
        return StatusCode(StatusCodes.Status200OK);
    }
}