using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot();

builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

var app = builder.Build();
app.MapGet("/", () => "This is the gateway home.");

app.UseOcelot().Wait();
app.Run();