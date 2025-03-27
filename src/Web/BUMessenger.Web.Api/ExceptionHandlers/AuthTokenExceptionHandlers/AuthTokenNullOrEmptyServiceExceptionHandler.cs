using BUMeesenger.Domain.Exceptions.Services.AuthTokenExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.AuthTokenExceptionHandlers;

public class AuthTokenNullOrEmptyServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is AuthTokenNullOrEmptyServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Refresh token не предоставлен.",
            statusCode = StatusCodes.Status400BadRequest
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}