namespace MySpace.Authentication.Domain.Models;

public class Token
{
    public string TokenString { get; set; } = null!;
    public DateTime ExpiresIn { get; set; }
}