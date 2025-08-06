using AutoMapper;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class ProblemMappingProfile : Profile
    {
        public ProblemMappingProfile()
        {
            CreateMap<Problem, ProblemDto>().ReverseMap();
        }
    }
}
