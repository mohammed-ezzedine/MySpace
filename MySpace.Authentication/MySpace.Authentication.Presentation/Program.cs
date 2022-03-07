using System.Security.Authentication;
using System.Text;
using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MySpace.Authentication.Application.Services;
using MySpace.Authentication.Domain.Configurations;
using MySpace.Authentication.Domain.Models;
using MySpace.Authentication.Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var persistenceConfiguration = configuration.GetSection(nameof(PersistenceConfiguration)).Get<PersistenceConfiguration>();
builder.Services.AddSingleton(persistenceConfiguration);

var identityConfiguration = configuration.GetSection(nameof(IdentityConfiguration)).Get<IdentityConfiguration>();
builder.Services.AddSingleton(identityConfiguration);

var adminConfiguration = configuration.GetSection(nameof(AdminConfiguration)).Get<AdminConfiguration>();
builder.Services.AddSingleton(adminConfiguration);

builder.Services.AddIdentityMongoDbProvider<User>(_ => { }, mongo =>
    {
        mongo.ConnectionString = persistenceConfiguration.ConnectionString + "/" +persistenceConfiguration.DatabaseName;
    })
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = identityConfiguration.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityConfiguration.Secret))
        };
    });

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.SeedUsers();

app.Run();