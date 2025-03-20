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

        return services;
    }
}