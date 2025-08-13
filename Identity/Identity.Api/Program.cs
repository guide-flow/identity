using DotNetEnv;
using Identity.Api.Startup;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

// Setup core Auth "module"
builder.Services.SetupAuth();

builder.Services.AddJwtAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var authContext = scope.ServiceProvider.GetRequiredService<AuthContext>();
    authContext.Database.Migrate();
}

app.Run();
