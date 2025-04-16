using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;
using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;
using BUMessenger.Web.Api.ExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.AuthTokenExceptionHandlers;
using BUMessenger.Web.Api.ExceptionHandlers.ChatExceptionHandlers;
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
        services.AddSingleton<IExceptionHandler, UserWrongPasswordServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, AuthTokenNotFoundServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, AuthTokenNullOrEmptyServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, AuthTokenExpiredServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, ExpiredApproveCodeUserServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, ChatNotFoundServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, UserNotInChatServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, EmptyChatNameServiceExceptionHandler>();
        services.AddSingleton<IExceptionHandler, DefaultExceptionHandler>();
        
        return services;
    }
}