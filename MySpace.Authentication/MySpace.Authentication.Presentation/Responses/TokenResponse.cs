using System.IdentityModel.Tokens.Jwt;

namespace MySpace.Authentication.Presentation.Responses;

public class TokenResponse
{
    public string Token { get; set; } = null!;
}