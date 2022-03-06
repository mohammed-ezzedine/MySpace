using AutoMapper;
using MySpace.Authentication.Domain.Models;
using MySpace.Authentication.Persistence.Entities;

namespace MySpace.Authentication.Persistence.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserEntity>();
        CreateMap<UserEntity, User>()
            .ForMember(dest => dest.Email, 
                opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.NormalizedEmail, 
                opt => opt.MapFrom(src => src.NormalizedUserName));
    }
}