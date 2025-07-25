using AutoMapper;
using DryIoc.ImTools;
using MentorAi_backd.Application.DTOs.ProfileDto;
using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class StudentProfileMappingProfile : Profile
    {
        public StudentProfileMappingProfile()
        {
            CreateMap<StudentUpdateDto, StudentProfile>().ReverseMap();



            CreateMap<StudentProfile, StudentProfileDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status))
            .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))

             .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Graduation, opt => opt.MapFrom(src => src.Graduation))
                .ForMember(dest => dest.GraduationYear, opt => opt.MapFrom(src => src.GraduationYear))
                .ForMember(dest => dest.University, opt => opt.MapFrom(src => src.University))
                .ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.Major))

                // Guardian properties
                .ForMember(dest => dest.GuardianName, opt => opt.MapFrom(src => src.GuardianName))
                .ForMember(dest => dest.GuardianEmail, opt => opt.MapFrom(src => src.GuardianEmail))
                .ForMember(dest => dest.GuardianPhoneNumber, opt => opt.MapFrom(src => src.GuardianPhoneNumber))
                .ForMember(dest => dest.GuardianRelationship, opt => opt.MapFrom(src => src.GuardianRelationship))
            //Problem stats
            .ForMember(dest => dest.ProblemsSolvedCount, opt => opt.MapFrom(src => src.ProblemAttempts
            .Where(pa => pa.Status == "Passed")
            .Select(pa => pa.ProblemId)
            .Distinct()
            .Count()))
            .ForMember(dest => dest.TotalChallengesAttempted, opt => opt.MapFrom(src => src.ProblemAttempts.Count))
            .ForMember(dest => dest.CertificationsEarnedCount, opt => opt.MapFrom(src =>
                src.StudentCertifications.Count()))

            .ForMember(dest => dest.BadgesEarnedCount, opt => opt.MapFrom(src =>
                src.UserBadges.Count()))

            //After map good fro null propagation and errrors free

            // Learning progress
            .AfterMap((src, dest) =>
            {
                var latest = src.RoadmapProgresses?
                    .OrderByDescending(rp => rp.LastActivityDate)
                    .FirstOrDefault();
                dest.CurrentRoadmapTitle = latest?.Roadmap?.Title ?? "Not Started";
                dest.CurrentRoadmapProgressPercentage = latest?.CurrentProgressPercentage ?? 0.0;
                dest.LastActiveModuleTitle = latest?.CurrentModule?.Title ?? "No Active LearningModule";
            });

            
        }
    }
}
