using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Configurations;
using MySpace.Api.Domain.Persistence;
using MySpace.Api.Persistence.Repositories;

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();