using Microsoft.AspNetCore.Identity;

namespace MySpace.Authentication.Domain.Models;

public class User : IdentityUser
{
    public void Update(User user)
    {
        PasswordHash = user.PasswordHash ?? PasswordHash;
        UserName = user.UserName ?? UserName;
        NormalizedUserName = UserName.ToUpper();
    }
}