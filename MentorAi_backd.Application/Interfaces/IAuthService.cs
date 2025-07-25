﻿using MentorAi_backd.Application.DTOs.AuthDto;


namespace MentorAi_backd.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterDto registerDto);
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<string>> LogoutAsync(int userId, string token);
        Task<ApiResponse<bool>> RevokeTokenAsync(int userId, string token);

        Task<ApiResponse<string>> VerifyEmailAsync(Guid token);
        Task<ApiResponse<string>> ResendVerificationEmailAsync(string email);
    }
}
