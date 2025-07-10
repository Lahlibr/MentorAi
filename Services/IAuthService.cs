using MentorAi_backd.DTO.AuthDto;
using MentorAi_backd.Models.Entity;

namespace MentorAi_backd.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterDto registerDto);
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<string>> VerifyEmailAsync(Guid token);
        Task<ApiResponse<string>> ResendVerificationEmailAsync(string email);

    }
}
