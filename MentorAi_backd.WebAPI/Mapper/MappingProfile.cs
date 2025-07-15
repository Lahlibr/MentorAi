using AutoMapper;
using MentorAi_backd.Application.DTOs.AuthDto;
using MentorAi_backd.Domain.Entities.UserEntity;

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
