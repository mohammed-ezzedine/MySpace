using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Configurations;
using MySpace.Api.Domain.Persistence;
using MySpace.Api.Persistence.Repositories;
using MySpace.Api.Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var persistenceConfiguration = configuration.GetSection(nameof(PersistenceConfiguration)).Get<PersistenceConfiguration>();
builder.Services.AddSingleton(persistenceConfiguration);

builder.Services.AddSingleton<TagRepository, MongoDbTagRepository>();
builder.Services.AddSingleton<ArticleRepository, MongoDbArticleRepository>();
builder.Services.AddSingleton<ITagService, TagService>();
builder.Services.AddSingleton<IArticleService, ArticleService>();
builder.Services.AddSingleton<ICommentService, CommentService>();

builder.Services.AddAutoMapper(
    typeof(Program).Assembly, 
    typeof(MongoDbArticleRepository).Assembly);

var identityConfiguration = configuration.GetSection(nameof(IdentityConfiguration)).Get<IdentityConfiguration>();
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

app.Run();