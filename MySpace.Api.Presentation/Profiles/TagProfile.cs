using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;

namespace MySpace.Api.Presentation.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagResponse>();
        CreateMap<TagRequest, Tag>();
    }
}