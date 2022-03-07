namespace MySpace.Authentication.Presentation.Responses;

public class TokenResponse
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresIn { get; set; }
}