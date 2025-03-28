using BUMeesenger.Domain.Exceptions.Services.AuthTokenExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.AuthTokenExceptionHandlers;

public class AuthTokenExpiredServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is AuthTokenExpiredServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Refresh token недействителен.",
            statusCode = StatusCodes.Status403Forbidden
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}