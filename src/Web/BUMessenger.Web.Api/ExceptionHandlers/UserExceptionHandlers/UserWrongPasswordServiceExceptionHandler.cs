using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.UserExceptionHandlers;

public class UserWrongPasswordServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is UserWrongPasswordServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Введён неправильный пароль.",
            statusCode = StatusCodes.Status403Forbidden
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}