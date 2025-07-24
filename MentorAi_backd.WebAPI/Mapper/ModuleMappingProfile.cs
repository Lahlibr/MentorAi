using AutoMapper;
using MentorAi_backd.Application.DTOs.ModulesDto;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class ModuleMappingProfile : Profile
    {
        public  ModuleMappingProfile()
        {
            CreateMap<CreateModuleDto, Modules>()
                .ForMember(dest => dest.Roadmap, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Modules, ModuleDto>()
                .ReverseMap();
            CreateMap<Modules, UpdateModuleDto>()
                .ReverseMap();
        }
    }
}
