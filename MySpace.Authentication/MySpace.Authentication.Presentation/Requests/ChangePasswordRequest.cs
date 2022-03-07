namespace MySpace.Authentication.Presentation.Requests;

public class ChangePasswordRequest
{
    public string Username { get; set; } = null!;
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}