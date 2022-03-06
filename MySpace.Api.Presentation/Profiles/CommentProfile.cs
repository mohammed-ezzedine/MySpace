using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;

namespace MySpace.Api.Presentation.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

        CreateMap<CommentRequest, Comment>();
    }
}