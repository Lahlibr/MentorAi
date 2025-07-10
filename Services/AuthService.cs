using MentorAi_backd.DTO.AuthDto;
using MentorAi_backd.Models.Entity;
using MentorAi_backd.Repositories.Interfaces;

namespace MentorAi_backd.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        public AuthService(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }
        public async Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
            return await _authRepo.RegisterAsync(registerDto);
        }
        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            return await _authRepo.LoginAsync(loginDto);
        }
        public async Task<ApiResponse<string>> VerifyEmailAsync(Guid token)
        {
            return await _authRepo.VerifyEmailAsync(token);
        }
        public async Task<ApiResponse<string>> ResendVerificationEmailAsync(string email)
        {
            return await _authRepo.ResendVerificationEmailAsync(email);
        }
    }
}
