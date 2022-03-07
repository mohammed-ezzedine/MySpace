using AutoMapper;
using MySpace.Authentication.Domain.Models;
using MySpace.Authentication.Presentation.Responses;

namespace MySpace.Authentication.Presentation.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.ToString()));
    }
}