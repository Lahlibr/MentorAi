using MentorAi_backd.Application.DTOs.AuthDto;
using MentorAi_backd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MentorAi_backd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private readonly IAuthService _authService;
       
       public AuthController(IAuthService authService)
       {
           _authService = authService;
       }
       [HttpPost("register")]
       public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
       {
           var result = await _authService.RegisterAsync(registerDto);
           return Ok(result);
       }
       [HttpPost("login")]
         public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
         {
              var result = await _authService.LoginAsync(loginDto);
              if(!result.Success)
              {
                  return Unauthorized(result.Message);
              }
              var loginData = result.Data;

              Response.Cookies.Append("access_token", loginData.AccessToken, new CookieOptions
              {
                  HttpOnly = true,
                  Secure = true,
                  SameSite = SameSiteMode.Strict,
                  Expires = DateTime.UtcNow.AddMinutes(60) 
              });
              Response.Cookies.Append("refresh_token", loginData.RefreshToken, new CookieOptions
              {
                  HttpOnly = true,
                  Secure = true,
                  SameSite = SameSiteMode.Strict,
                  Expires = DateTime.UtcNow.AddDays(30) 
              });
            return Ok(result);
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] Guid token)
        {
            var result = await _authService.VerifyEmailAsync(token);
            return Ok(result);
        }
        [HttpPost("resend-verification-email")]
        public async Task<IActionResult> ResendVerification([FromBody] string email)
        {
            var result = await _authService.ResendVerificationEmailAsync(email);
            return Ok(result);
        }
       [HttpPost("logout")]
[Authorize]
public async Task<IActionResult> Logout([FromBody] LogoutRequestDto logoutDto)
{
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
    {
        return Unauthorized("User not authenticated.");
    }

    if (string.IsNullOrEmpty(logoutDto.RefreshToken))
    {
        return BadRequest("Refresh token is required for logout.");
    }

    var logoutResult = await _authService.LogoutAsync(userId, logoutDto.RefreshToken);
    return Ok(logoutResult);
}

    }
}
