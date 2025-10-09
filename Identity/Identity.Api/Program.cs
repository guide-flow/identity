using Core.UseCase;
using DotNetEnv;
using Identity.Api.Startup;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using NATS.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddSingleton<IConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var url = config.GetValue<string>("NATS_URL") ?? "nats://localhost:4222";
    var cf = new ConnectionFactory();
    return cf.CreateConnection(url);
});

builder.Services.AddSingleton<IdentitySagaHandler>();

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
var identityHandler =  app.Services.GetRequiredService<IdentitySagaHandler>();
identityHandler.Subscribe();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();
