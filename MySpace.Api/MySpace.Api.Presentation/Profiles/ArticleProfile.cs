using System.Text.Json.Nodes;
using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;
using MySpace.Api.Presentation.Utils;

namespace MySpace.Api.Presentation.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleResponse>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags == null? Array.Empty<string>() : src.Tags.Select(t => t.Name)));
        
        CreateMap<ArticleRequest, Article>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(_ => "Mohammed Ezzedine"))
            .ForMember(dest => dest.Tags, 
                opt => opt.MapFrom(
                    src => 
                        src.Tags == null 
                            ? new List<Tag>() 
                            : src.Tags.Select(t => new Tag(t, 0))));
    }
}