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
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("User not authenticated.");
            }


            var result = await _authService.LogoutAsync(userId);
            return Ok(result);
        }
    }
}
