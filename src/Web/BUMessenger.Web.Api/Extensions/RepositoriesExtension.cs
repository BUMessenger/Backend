using BUMessenger.DataAccess.Repositories.Repositories;
using BUMessenger.Domain.Interfaces.Repositories;

namespace BUMessenger.Web.Api.Extensions;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUnregisteredUserRepository, UnregisteredUserRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}