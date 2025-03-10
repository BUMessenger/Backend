using System.Text.Json;
using System.Text.Json.Serialization;
using BUMessenger.DataAccess.Context;
using BUMessenger.Web.Api.Extensions;
using BUMessenger.Web.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
});

var connectionString = Environment.GetEnvironmentVariable("DOCKER_CONNECTION_STRING") 
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
        
builder.Services.AddDbContext<BUMessengerContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddExceptionHandlers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }

        await next();
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<BUMessengerContext>();

if (!dbContext.Database.CanConnect())
    Console.WriteLine("Error: Can't connect to database");
else
    Console.WriteLine("Connected to database");

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
