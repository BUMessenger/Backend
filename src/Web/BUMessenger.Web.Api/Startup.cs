using System.Text.Json;
using System.Text.Json.Serialization;
using BUMessenger.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace BUMessenger.Web.Api;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
            });
        
        services.AddDbContext<BUMessengerContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
    }
}