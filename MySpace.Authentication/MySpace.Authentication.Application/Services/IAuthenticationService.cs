using System.IdentityModel.Tokens.Jwt;
using MySpace.Authentication.Domain.Models;

namespace MySpace.Authentication.Application.Services;

public interface IAuthenticationService
{
    IEnumerable<User> GetUsers();
    Task RegisterUser(string username, string password);
    Task ChangePassword(string username, string oldPassword, string newPassword);
    Task DeleteUser(string username);
    Task<Token> Login(string username, string password);
}