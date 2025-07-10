using MentorAi_backd.Data;
using MentorAi_backd.DTO.AuthDto;
using MentorAi_backd.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
    }
}
