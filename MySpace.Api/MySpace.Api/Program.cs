using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using MySpace.Api.Application.Configurations;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Application.Services;
using MySpace.Api.Configurations;
using MySpace.Api.Persistence.Repositories;
using MySpace.Api.Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables(prefix: "MySpace_");

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.KnownProxies.Add(IPAddress.Parse("157.245.46.30"));
});

builder.Services.AddControllers().AddApplicationPart(typeof(ArticleController).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var persistenceConfiguration = configuration.GetSection(nameof(PersistenceConfiguration)).Get<PersistenceConfiguration>();
builder.Services.AddSingleton(persistenceConfiguration);

builder.Services.AddSingleton<TagRepository, MongoDbTagRepository>();
builder.Services.AddSingleton<ArticleRepository, MongoDbArticleRepository>();
builder.Services.AddSingleton<ProjectRepository, MongoDbProjectRepository>();
builder.Services.AddSingleton<JobRepository, MongoDbJobRepository>();
builder.Services.AddSingleton<CounterRepository, MongoDbCounterRepository>();

builder.Services.AddSingleton<ITagService, TagService>();
builder.Services.AddSingleton<IArticleService, ArticleService>();
builder.Services.AddSingleton<ICommentService, CommentService>();
builder.Services.AddSingleton<IProjectService, ProjectService>();
builder.Services.AddSingleton<IJobService, JobService>();

builder.Services.AddAutoMapper(
    typeof(ArticleController).Assembly, 
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
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidIssuer = identityConfiguration.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityConfiguration.Secret))
        };
    });

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder =>
            corsPolicyBuilder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

var app = builder.Build();

app.UseForwardedHeaders();
app.UsePathBase("/");

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();