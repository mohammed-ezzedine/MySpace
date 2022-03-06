namespace MySpace.Authentication.Persistence.Entities;

public class UserEntity
{
    public string Id { get; set; } = null!;
    
    public string UserName { get; set; } = null!;

    public string NormalizedUserName { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string SecurityStamp { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}