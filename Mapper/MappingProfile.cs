using AutoMapper;
using MentorAi_backd.DTO.AuthDto;
using MentorAi_backd.Models.Entity.UserEntity;

namespace MentorAi_backd.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterResponseDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
        }
    }
}
