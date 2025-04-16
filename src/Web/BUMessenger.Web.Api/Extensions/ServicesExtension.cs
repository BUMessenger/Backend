using BUMessenger.Application.Services.Services;
using BUMessenger.Domain.Interfaces.Services;

namespace BUMessenger.Web.Api.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUnregisteredUserService, UnregisteredUserService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthTokenService, AuthTokenService>();
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IMessageService, MessageService>();

        return services;
    }
}