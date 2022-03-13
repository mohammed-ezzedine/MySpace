using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MySpace.Authentication.Domain.Configurations;
using MySpace.Authentication.Domain.Exceptions;
using MySpace.Authentication.Domain.Models;
using InvalidCredentialException = MySpace.Authentication.Domain.Exceptions.InvalidCredentialException;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MySpace.Authentication.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly IdentityConfiguration _identityConfiguration;

    public AuthenticationService(
        UserManager<User> userManager, 
        IdentityConfiguration identityConfiguration)
    {
        _userManager = userManager;
        _identityConfiguration = identityConfiguration;
    }

    public IEnumerable<User> GetUsers()
    {
        return _userManager.Users.AsEnumerable();
    }

    public async Task RegisterUser(string username, string password)
    {
        await ThrowExceptionIfUsernameExists(username);
        var user = new User
        {
            UserName = username,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        var result = await _userManager.CreateAsync(user, password);
        ThrowExceptionIfRegistrationFailed(result);
    }

    public async Task ChangePassword(string username, string oldPassword, string newPassword)
    {
        var user = await GetUserOrThrowExceptionIfNotExisting(username);
        await CheckCredentialsAndThrowExceptionIfInvalid(user, oldPassword);
        
        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        if (!result.Succeeded)
        {
            throw new ChangePasswordFailedException(result.Errors);
        }
    }

    public async Task DeleteUser(string username)
    {
        var user = await GetUserByUsernameOrDefault(username);
        ThrowExceptionIfOnlyOneUserExists();

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new DeleteUserFailedException(result.Errors);
        }
    }

    private void ThrowExceptionIfOnlyOneUserExists()
    {
        if (_userManager.Users.Count() < 2)
        {
            throw new DeleteUserFailedException("No other users exist");
        }
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await GetUserOrThrowExceptionIfNotExisting(username);
        await CheckCredentialsAndThrowExceptionIfInvalid(user, password);

        var authClaims = GetJwtAuthClaims(user);
        var authSigningKey = GetAuthSigningKey();

        var token = GenerateJwtSecurityToken(authClaims, authSigningKey);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GenerateJwtSecurityToken(List<Claim> authClaims, SymmetricSecurityKey authSigningKey)
    {
        return new JwtSecurityToken(
            issuer: _identityConfiguration.Issuer,
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
    }

    private SymmetricSecurityKey GetAuthSigningKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfiguration.Secret));
    }

    private static List<Claim> GetJwtAuthClaims(User user)
    {
        return new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
    }

    private async Task CheckCredentialsAndThrowExceptionIfInvalid(User user, string password)
    {
        var validCredentials = await _userManager.CheckPasswordAsync(user, password);
        if (!validCredentials)
        {
            throw new InvalidCredentialException();
        }
    }

    private async Task<User> GetUserOrThrowExceptionIfNotExisting(string username)
    {
        var user = await GetUserByUsernameOrDefault(username);

        if (user is null)
        {
            throw new UserNotFoundException(username);
        }

        return user;
    }

    private async Task<User> GetUserByUsernameOrDefault(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    private async Task ThrowExceptionIfUsernameExists(string username)
    {
        var user = await GetUserByUsernameOrDefault(username);
        if (user is not null)
        {
            throw new UsernameAlreadyExistsException(username);
        }
    }

    private static void ThrowExceptionIfRegistrationFailed(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            throw new RegisteringUserFailedException(result.Errors);
        }
    }
}