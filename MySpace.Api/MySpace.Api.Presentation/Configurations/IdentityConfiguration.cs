﻿namespace MySpace.Api.Presentation.Configurations;

public class IdentityConfiguration
{
    public string Issuer { get; set; } = null!;
    public string Secret { get; set; } = null!;
}