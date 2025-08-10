using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using org.omg.PortableInterceptor;


namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class ProblemService  : IProblemService
    {
        private readonly MentorAiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProblemService> _logger;
        private readonly IGenericRepository<Problem> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProblemService(MentorAiDbContext context, IMapper mapper, ILogger<ProblemService> logger, IGenericRepository<Problem> repository, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<ProblemDto>> GetAllProblemAsync(int id)
        {
            var problem = await _repository.GetByIdAsync(id);
            if (problem == null)
                throw new NotFoundException("Problem not found.");
            var res = _mapper.Map<ProblemDto>(problem);
            return ApiResponse<ProblemDto>.SuccessResponse(res, "Fetched Successfully");
        }
        public async Task<ApiResponse<ProblemDto>> CreateAsync(CreateProblemDto dto)
        {
            if (dto == null)
                throw new BadRequestException("LearningModule data cannot be null.");
            var problem = _mapper.Map<Problem>(dto);
            await _repository.AddAsync(problem);
            

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Creating Problem");
                return ApiResponse<ProblemDto>.ErrorResponse($"Error on Creating Problem: {ex.Message}");
            }

            var createDto = _mapper.Map<ProblemDto>(problem);
            return ApiResponse<ProblemDto>.SuccessResponse(createDto, "Added Successfully");

        }

        public async Task<ApiResponse<List<ProblemDto>>> GetAllAsync()
        {
            var allprob = await _repository.GetAllAsync();
            var result = _mapper.Map<List<ProblemDto>>(allprob);
            return ApiResponse<List<ProblemDto>>.SuccessResponse(result, "Fetched Successfully");
        }

        public async Task<ApiResponse<ProblemDto>> UpdateAsync(int id, CreateProblemDto dto)
        {
            var problem = await _repository.GetByIdAsync(id);
            if (problem == null)
                throw new NotFoundException("Problem Not Found");

            _mapper.Map(dto, problem);
            try
            {
                _repository.UpdateAsync(problem);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error on Updating Problem");
                return ApiResponse<ProblemDto>.ErrorResponse("Error Updating Problem");
            }
            var result =_mapper.Map<ProblemDto>(problem);
            return ApiResponse<ProblemDto>.SuccessResponse(result, "Update Success fully");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var problem = await _repository.GetByIdAsync(id);
            if (problem == null)
                throw new NotFoundException("Problem not found.");
            _repository.Delete(problem);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Deleting Problem");
                return ApiResponse<string>.ErrorResponse("Error on Deleting Problem");
            }

            return ApiResponse<string>.SuccessResponse("Deleted Successfully");
        }
    }

   
}
