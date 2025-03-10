namespace BUMessenger.Web.Api.ExceptionHandlers;

public interface IExceptionHandler
{
    bool CanHandle(Exception exception);
    Task HandleExceptionAsync(HttpContext context, Exception exception);
}