using AutoMapper;
using MentorAi_backd.Application.DTOs.RoadmapDto;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class RoadmapMappingProfile : Profile
    {
        public RoadmapMappingProfile()
        {
            CreateMap<CreateRoadmapDto, Roadmap>()
                .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules))
                .ReverseMap();
            CreateMap<Roadmap, RoadmapDto>()
                .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules))
                .ReverseMap();
            CreateMap<Roadmap, UpdateRoadmapDto>()
                .ReverseMap();
        }
    }
}
