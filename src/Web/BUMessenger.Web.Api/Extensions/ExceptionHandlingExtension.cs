using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;
using BUMessenger.Web.Api.ExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.DefaultExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.EmailExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.UnregisteredUserExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.UserExceptionHandlers;

namespace BUMessenger.Web.Api.Extensions;

public static class ExceptionHandlingExtension
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionHandler, UnregisteredUserAlreadyExistsServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, ReceiverDoesntExistEmailServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, UnregisteredUserNotFoundServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, WrongApproveCodeUserServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, UserAlreadyExistsServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, UserNotFoundServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, DefaultExceptionHandler>();

        return services;
    }
}