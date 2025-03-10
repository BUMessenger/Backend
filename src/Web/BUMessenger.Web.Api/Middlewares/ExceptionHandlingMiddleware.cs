using BUMessenger.Web.Api.ExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.DefaultExceptionHandlers;

namespace BUMessenger.Web.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IEnumerable<IExceptionHandler> _exceptionHandlers;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        IEnumerable<IExceptionHandler> exceptionHandlers)
    {
        _next = next;
        _exceptionHandlers = exceptionHandlers;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var handler = _exceptionHandlers.FirstOrDefault(h => h.CanHandle(exception))
                      ?? _exceptionHandlers.OfType<DefaultExceptionHandler>().First();

        await handler.HandleExceptionAsync(context, exception);
    }
}