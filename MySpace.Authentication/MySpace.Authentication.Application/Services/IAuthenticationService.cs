using System.IdentityModel.Tokens.Jwt;
using MySpace.Authentication.Domain.Models;

namespace MySpace.Authentication.Application.Services;

public interface IAuthenticationService
{
    Task RegisterUser(string username, string password);
    Task ChangePassword(string username, string oldPassword, string newPassword);
    Task DeleteUser(string username);
    Task<JwtSecurityToken> Login(string username, string password);
}