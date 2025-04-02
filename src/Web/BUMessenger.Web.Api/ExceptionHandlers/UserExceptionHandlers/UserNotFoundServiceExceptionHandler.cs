using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.UserExceptionHandlers;

public class UserNotFoundServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is UserNotFoundServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Пользователь не найден.",
            statusCode = StatusCodes.Status404NotFound
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}