using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.UnregisteredUserExceptionHandlers;

public class UnregisteredUserAlreadyExistsServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is UnregisteredUserAlreadyExistsServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status409Conflict;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Незарегистрированный пользователь с такой электронной почтой уже существует.",
            statusCode = StatusCodes.Status409Conflict
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}