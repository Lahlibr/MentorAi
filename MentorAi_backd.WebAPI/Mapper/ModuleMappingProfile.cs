using AutoMapper;
using MentorAi_backd.Application.DTOs.ModulesDto;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class ModuleMappingProfile : Profile
    {
        public  ModuleMappingProfile()
        {
            CreateMap<CreateModuleDto, LearningModule>()
                
                .ReverseMap();
            CreateMap<LearningModule, ModuleDto>()
                .ReverseMap();
            CreateMap<LearningModule, UpdateModuleDto>()
                .ReverseMap();
        }
    }
}
