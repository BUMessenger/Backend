using BUMessenger.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace BUMessenger.Web.Api.Middlewares;

public class RefreshTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;

    public RefreshTokenMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _scopeFactory = scopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
        {
            await _next(context);
            return;
        }
        
        if (context.User.Identity?.IsAuthenticated == true)
        {
            using var scope = _scopeFactory.CreateScope();
            var authTokenService = scope.ServiceProvider.GetRequiredService<IAuthTokenService>();

            var refreshTokenId = context.User.FindFirst("RefreshTokenId")?.Value;

            if (string.IsNullOrEmpty(refreshTokenId) || ! await authTokenService.IsRefreshTokenValidByIdAsync(Guid.Parse(refreshTokenId)))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = "Refresh токен невалидный или просроченный." });
                return;
            }
        }
        
        await _next(context);
    }
}