using System.Text.Json;
using System.Text.Json.Serialization;
using BUMessenger.DataAccess.Context;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<BUMessengerContext>();

if (!dbContext.Database.CanConnect())
    Console.WriteLine("Error: Can't connect to database");
else
    Console.WriteLine("Connected to database");

if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
