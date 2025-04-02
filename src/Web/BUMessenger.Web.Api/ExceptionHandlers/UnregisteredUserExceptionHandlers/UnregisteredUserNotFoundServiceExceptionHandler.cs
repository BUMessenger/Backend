using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.UnregisteredUserExceptionHandlers;

public class UnregisteredUserNotFoundServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is UnregisteredUserNotFoundServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Незарегистрированный пользователь не найден.",
            statusCode = StatusCodes.Status404NotFound
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}