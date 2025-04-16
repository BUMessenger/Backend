using BUMessenger.DataAccess.Repositories.Repositories;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Interfaces.Services;

namespace BUMessenger.Web.Api.Extensions;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUnregisteredUserRepository, UnregisteredUserRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAuthTokenRepository, AuthTokenRepository>();
        services.AddTransient<IChatRepository, ChatRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();

        return services;
    }
}