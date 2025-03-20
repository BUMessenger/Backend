using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

namespace BUMessenger.Web.Api.ExceptionHandlers.UserExceptionHandlers;

public class WrongApproveCodeUserServiceExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is WrongApproveCodeUserServiceException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Введён неправильный код подтверждения.",
            statusCode = StatusCodes.Status403Forbidden
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}