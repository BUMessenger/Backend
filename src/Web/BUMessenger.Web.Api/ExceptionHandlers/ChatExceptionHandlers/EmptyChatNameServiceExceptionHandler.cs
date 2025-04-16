using BUMeesenger.Domain.Exceptions.Services.ChatServiceException;

namespace BUMessenger.Web.Api.ExceptionHandlers.ChatExceptionHandlers;

public class EmptyChatNameServiceExceptionHandler : IExceptionHandler 
{
    public bool CanHandle(Exception exception)
    {
        return exception is EmptyChatNameServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Имя чата не может быть пустым.",
            statusCode = StatusCodes.Status400BadRequest
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}