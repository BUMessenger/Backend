namespace BUMessenger.Web.Api.ExceptionHandlers.DefaultExceptionHandlers;

public class ArgumentNullExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is ArithmeticException;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Произошла внутренняя ошибка сервера." + exception.Message,
            statusCode = StatusCodes.Status500InternalServerError
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}