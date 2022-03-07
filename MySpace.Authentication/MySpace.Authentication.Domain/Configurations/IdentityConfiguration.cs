namespace MySpace.Authentication.Domain.Configurations;

public class IdentityConfiguration
{
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public string Secret { get; set; } = null!;
}