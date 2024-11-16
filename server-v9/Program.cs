using Server_V9.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy  =>
        {
            policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()
                  .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        });
});

builder.Services.AddSignalR();

var app = builder.Build();

// app.UseRouting();

app.UseCors();

app.MapHub<ChatHub>("/chat");

app.Run("http://localhost:5170");
