using BUMessenger.Web.Api.ExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.DefaultExceptionHandlers;

namespace BUMessenger.Web.Api.Extensions;

public static class ExceptionHandlingExtension
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionHandler, ArgumentNullExceptionHandler>();
        services.AddSingleton<IExceptionHandler, DefaultExceptionHandler>();

        return services;
    }
}