using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagEntity>();
        CreateMap<TagEntity, Tag>();
    }
}