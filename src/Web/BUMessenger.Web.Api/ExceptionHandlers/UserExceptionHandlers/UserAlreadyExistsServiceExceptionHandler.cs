using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.UserExceptionHandlers;

public class UserAlreadyExistsServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is UserAlreadyExistsServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status409Conflict;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Пользователь с такой электронной почтой уже существует.",
            statusCode = StatusCodes.Status409Conflict
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}