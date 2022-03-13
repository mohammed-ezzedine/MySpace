using System.Text.Json;
using System.Text.Json.Nodes;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleEntity>()
            .ForMember(dest => dest.Id, 
                option => option.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Content, 
                option => option.MapFrom(
                    src => BsonSerializer.Deserialize<BsonArray>(src.Content.ToString(), null)));

        CreateMap<ArticleEntity, Article>()
            .ForMember(dest => dest.Id,
                option => option.MapFrom(src => new ArticleId(src.Id)))
            .ForMember(dest => dest.Content,
                option => option.MapFrom(
                    src => JsonArray.Parse(src.Content.ToString(), null, default)));
    }
}