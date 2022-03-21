using AutoMapper;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;

namespace MySpace.Api.Presentation.Profiles;

public class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, JobResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        
        CreateMap<JobRequest, Job>();
    }
}