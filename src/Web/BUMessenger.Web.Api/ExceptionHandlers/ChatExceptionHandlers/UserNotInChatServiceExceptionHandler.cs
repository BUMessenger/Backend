using BUMeesenger.Domain.Exceptions.Services.ChatServiceException;

namespace BUMessenger.Web.Api.ExceptionHandlers.ChatExceptionHandlers;

public class UserNotInChatServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is UserNotInChatServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Пользователь не состоит в чате.",
            statusCode = StatusCodes.Status404NotFound,
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}