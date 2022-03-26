using System.Text.Json.Nodes;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Content, 
            option => option.MapFrom(
                src => BsonSerializer.Deserialize<BsonArray>(src.Content.ToString(), null)));
        
        CreateMap<ProjectEntity, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ProjectId(src.Id)))
            .ForMember(dest => dest.Content,
                option => option.MapFrom(
                    src => JsonArray.Parse(src.Content.ToString(), null, default)));
    }    
}