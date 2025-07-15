using AutoMapper;
using MentorAi_backd.Application.DTOs.AuthDto;
using MentorAi_backd.Application.DTOs.ProfileDto;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.Mapper
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<User, RegisterResponseDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginResponseDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();

            CreateMap<User, StudentProfileDto>().ReverseMap();

        }
    }
}
