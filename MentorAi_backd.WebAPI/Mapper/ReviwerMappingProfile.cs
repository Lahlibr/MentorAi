using AutoMapper;
using MentorAi_backd.Application.DTOs.ReviwerDto;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class ReviwerMappingProfile : Profile
    {
        public ReviwerMappingProfile() { 
        CreateMap<ReviewerProfile, ReviewerProfileDto>().ReverseMap();
        }

    }
}
