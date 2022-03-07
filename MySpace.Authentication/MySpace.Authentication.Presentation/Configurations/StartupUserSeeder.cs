using MySpace.Authentication.Application.Seeders;
using MySpace.Authentication.Application.Services;
using MySpace.Authentication.Domain.Configurations;

namespace MySpace.Authentication.Presentation.Configurations;

public static class StartupUserSeeder
{
    public static void SeedUsers(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var authenticationService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
        var adminConfiguration = scope.ServiceProvider.GetRequiredService<AdminConfiguration>();
        var userSeeder = new UserSeeder(authenticationService, adminConfiguration);
        userSeeder.AddUserIfNonExists().Wait();
    }
}