using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentEntity>()
            .ForMember(dest => dest.Id, 
                option => option.MapFrom(src => src.Id.Value));

        CreateMap<CommentEntity, Comment>()
            .ForMember(dest => dest.Id, 
                option => option.MapFrom(src => new CommentId(src.Id)));
    }
}