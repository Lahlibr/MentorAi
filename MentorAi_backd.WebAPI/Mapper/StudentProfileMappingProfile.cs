using AutoMapper;
using DryIoc.ImTools;
using MentorAi_backd.Application.DTOs.ProfileDto;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class StudentProfileMappingProfile : Profile
    {
        public StudentProfileMappingProfile()
        {
           
            CreateMap<User, StudentProfileDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.StudentProfile.Age))
            .ForMember(dest => dest.AssessmentScore, opt => opt.MapFrom(src => src.StudentProfile.AssessmentScore))
            .ForMember(dest => dest.CurrentLearningGoal, opt => opt.MapFrom(src => src.StudentProfile.CurrentLearningGoal))
            .ForMember(dest => dest.PreferredLearningStyle, opt => opt.MapFrom(src => src.StudentProfile.PreferredLearningStyle))
            .ReverseMap(); 
        }
    }
}
