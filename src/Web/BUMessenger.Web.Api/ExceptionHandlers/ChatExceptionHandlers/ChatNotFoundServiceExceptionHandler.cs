using BUMeesenger.Domain.Exceptions.Services.ChatServiceException;

namespace BUMessenger.Web.Api.ExceptionHandlers.ChatExceptionHandlers;

public class ChatNotFoundServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is ChatNotFoundServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Чат не найден.",
            statusCode = StatusCodes.Status404NotFound
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}