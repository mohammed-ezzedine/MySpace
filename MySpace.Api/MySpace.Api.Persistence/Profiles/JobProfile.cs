using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Profiles;

public class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, JobEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        
        CreateMap<JobEntity, Job>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new JobId(src.Id)));
    }
}