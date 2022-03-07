
using MySpace.Authentication.Application.Services;
using MySpace.Authentication.Domain.Configurations;

namespace MySpace.Authentication.Application.Seeders;

public class UserSeeder
{
    private readonly IAuthenticationService _authenticationService;
    private readonly AdminConfiguration _adminConfiguration;

    public UserSeeder(IAuthenticationService authenticationService, AdminConfiguration adminConfiguration)
    {
        _authenticationService = authenticationService;
        _adminConfiguration = adminConfiguration;
    }

    public async Task AddUserIfNonExists()
    {
       var usersExist = _authenticationService.GetUsers().Any();
       if (!usersExist)
       {
           await _authenticationService.RegisterUser(_adminConfiguration.Username, _adminConfiguration.Password);
       }
    }
}