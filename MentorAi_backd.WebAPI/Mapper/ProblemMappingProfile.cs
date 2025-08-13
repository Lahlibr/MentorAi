using AutoMapper;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Infrastructure.Utilities;
using Newtonsoft.Json;

namespace MentorAi_backd.WebAPI.Mapper
{
    public class ProblemMappingProfile : Profile
    {
        public ProblemMappingProfile()
        {

            CreateMap<Problem, ProblemDto>()
                .ForMember(dest => dest.ExampleInputs,
                    opt =>
                        opt.MapFrom(src => JsonConvert.DeserializeObject<List<string>>(src.ExampleInputsJson ?? "[]")))
                .ForMember(dest => dest.ExampleOutputs,
                    opt => opt.MapFrom(src =>
                        JsonConvert.DeserializeObject<List<string>>(src.ExampleOutputsJson ?? "[]")))
                .ForMember(dest => dest.Difficulty,
                    opt => opt.MapFrom(src => src.DifficultyLevel))
                .ForMember(dest => dest.LanguageSolutions, opt => opt.MapFrom(src => src.LanguageSolutions
                    .Select(lang =>
                        new ProblemLanguageSolution
                        {
                            Language = lang.Language.ToLowerInvariant(),
                            SolutionTemplate = lang.SolutionTemplate,
                            ExpectedSolutionHash =
                                SolutionHashHelper.ComputeHash(lang.SolutionTemplate) // <-- hash computed here
                        }).ToList()
                ));

            CreateMap<CreateProblemDto, Problem>()
                .ForMember(dest => dest.ExampleInputsJson,
                    opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.ExampleInputs)))
                .ForMember(dest => dest.ExampleOutputsJson,
                    opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.ExampleOutputs)))
                .ForMember(dest => dest.HiddenTestCasesJson,
                    opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.HiddenTestCases)))
                .ForMember(dest => dest.ExampleTestCasesJson,
                    opt => opt.MapFrom(src => JsonConvert.SerializeObject(
                        src.ExampleInputs.Zip(src.ExampleOutputs, (input, output) =>
                            new { input, output }))))
                .ForMember(dest => dest.DifficultyLevel,
                    opt => opt.MapFrom(src => src.Difficulty))
                


                .ForMember(dest => dest.LanguageSolutions, opt => opt.MapFrom(src => src.LanguageSolutions.Select(lang =>
                    new ProblemLanguageSolution
                    {
                        Language = lang.Language.ToLowerInvariant(),
                        SolutionTemplate = lang.SolutionTemplate,
                        ExpectedSolutionHash = SolutionHashHelper.ComputeHash(lang.SolutionTemplate)
                    })
                .ToList()
            ));
            CreateMap<LanguageSolutionDto, ProblemLanguageSolution>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProblemId, opt => opt.Ignore())
                .ForMember(dest => dest.Problem, opt => opt.Ignore())
                .ReverseMap();
        }
    }
    
}