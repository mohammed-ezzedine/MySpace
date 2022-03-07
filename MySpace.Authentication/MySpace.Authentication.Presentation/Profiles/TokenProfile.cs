using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using MySpace.Authentication.Domain.Models;
using MySpace.Authentication.Presentation.Responses;

namespace MySpace.Authentication.Presentation.Profiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<Token, TokenResponse>()
            .ForMember(dest => dest.Token,
                opt => opt.MapFrom(src => src.TokenString));
    }
}