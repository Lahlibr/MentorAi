using AutoMapper;
using MentorAi_backd.Application.DTOs.AdminDto;
using MentorAi_backd.Application.DTOs.ReviwerDto;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class ReviwerMappingProfile : Profile
    {
        public ReviwerMappingProfile() { 
        CreateMap<ReviewerProfile, ReviewerProfileDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status))
            .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))

             
                .ReverseMap();
            CreateMap<ReviewerProfile, ReviewerUpdateDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ReverseMap();

            CreateMap<ReviewerProfile, ReviewerAdminViewDto>().ReverseMap();
        }

    }
}
