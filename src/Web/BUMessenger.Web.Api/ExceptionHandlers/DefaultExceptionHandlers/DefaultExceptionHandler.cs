namespace BUMessenger.Web.Api.ExceptionHandlers.DefaultExceptionHandlers;

public class DefaultExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return false;
    }
    
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Произошла внутренняя ошибка сервера.",
            statusCode = StatusCodes.Status500InternalServerError
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}