﻿namespace MySpace.Authentication.Presentation.Requests;

public class RegisterRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}