using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;

namespace MySpace.Api.Presentation.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        
        CreateMap<ArticleRequest, Article>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(_ => "Mohammed Ezzedine"));
    }
}