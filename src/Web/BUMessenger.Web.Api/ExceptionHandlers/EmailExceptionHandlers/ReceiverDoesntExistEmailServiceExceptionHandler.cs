using BUMeesenger.Domain.Exceptions.Services.EmailServiceExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.EmailExceptionHandlers;

public class ReceiverDoesntExistEmailServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is ReceiverDoesntExistEmailServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Неверно указана электронная почта.",
            statusCode = StatusCodes.Status400BadRequest
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}