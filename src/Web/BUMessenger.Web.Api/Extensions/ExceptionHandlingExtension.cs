using BUMessenger.Web.Api.ExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.DefaultExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.EmailExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.UnregisteredUserExceptionHandlers;

namespace BUMessenger.Web.Api.Extensions;

public static class ExceptionHandlingExtension
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionHandler, UnregisteredUserAlreadyExistsServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, ReceiverDoesntExistEmailServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, DefaultExceptionHandler>();

        return services;
    }
}