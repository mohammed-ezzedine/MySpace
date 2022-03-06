using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleEntity>()
            .ForMember(dest => dest.Id, 
                option => option.MapFrom(src => src.Id.Value));

        CreateMap<ArticleEntity, Article>()
            .ForMember(dest => dest.Id, 
                option => option.MapFrom(src => new ArticleId(src.Id)));
    }
}