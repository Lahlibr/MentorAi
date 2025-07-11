using AutoMapper;
using MentorAi_backd.Data;
using MentorAi_backd.DTO.AuthDto;
using MentorAi_backd.Exceptions;
using MentorAi_backd.Models.Entity;
using MentorAi_backd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace MentorAi_backd.Repositories.Implementations
{
    public class AuthRepo : IAuthRepo
    {
        private readonly MentorAiDbContext _context;
        private readonly ITokenRepo _TokenRepo;
        private readonly ILogger<AuthRepo> _logger;
        private readonly IMapper _mapper;

        public AuthRepo(MentorAiDbContext context, ITokenRepo tokenRepo, ILogger<AuthRepo> logger,IMapper _mapper)
        {
            _context = context;
            _TokenRepo = tokenRepo;
            _logger = logger;
            _mapper = _mapper;
        }
        public async Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == registerDto.UserName))
            {
                throw new ConflictException("User with this Username already exists");
            }
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new ConflictException("User with this Email already exists");
            }

            var password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var newUser = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Password = password,
                UserRole = registerDto.Role,
                VerificationToken = Guid.NewGuid(),
                VerificationTokenExpiry = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };

            try
            {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"User {newUser.UserName} registered. Verification token: {newUser.VerificationToken}");

                var response = _mapper.Map<RegisterResponseDto>(newUser);
                response.Message = "Registration successful. Please check your email for verification link.";

                return ApiResponse<RegisterResponseDto>.SuccessResponse(response, response.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update failed during registration");
                throw new DatabaseException("An error occurred while saving the user to the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during registration");
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ApiResponse<LoginResponseDto>>LoginAsync(LoginDto loginDto){
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email && !u.IsDeleted);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                // Implement lockout logic here
                user.FailedLoginAttempts++;
                user.LastFailedLogin = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                throw new UnauthorizedException("Invalid credentials."); }
            if (user.Status == Models.Enum.AccountStatus.LockedOut)
            {
                if (user.LockoutEnd > DateTime.UtcNow)
                {
                    throw new ForbiddenException($"Account locked out. Try again after {user.LockoutEnd.Value.Subtract(DateTime.UtcNow).Minutes} minutes.");
                }
                else
                {
                    user.Status = Models.Enum.AccountStatus.Active;
                    user.FailedLoginAttempts = 0;
                    user.LockoutEnd = null;
                }
            }
            //if (user.Status == Models.Enum.AccountStatus.PendingVerification)
            //{
            //    throw new ForbiddenException("Account not verified. Please check your email for verification link.");
            //}
            if(user.Status == Models.Enum.AccountStatus.Blocked)
            {
                throw new ForbiddenException("Account is blocked. Contact support for assistance.");
            }
            user.FailedLoginAttempts = 0;
            user.LastSuccessfulLogin = DateTime.UtcNow;

            var accessToken = _TokenRepo.GenerateAccessToken(user);
            var refreshToken = _TokenRepo.GenerateRefreshToken();
            
            user.RefreshToken = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            user.CreatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            
            var loginResponse = new LoginResponseDto
            {
                UserName = user.UserName,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserRole = user.UserRole.ToString(),
                ProfileImageUrl = user.ProfileImageUrl
            };
            return ApiResponse<LoginResponseDto>.SuccessResponse(
                loginResponse, "Login successful. Welcome back!");
        }

        public async Task<ApiResponse<string>> VerifyEmailAsync(Guid token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

            if (user == null)
            {
                throw new NotFoundException("User not found or invalid verification token.");
            }
            if (user.VerificationTokenExpiry < DateTime.UtcNow)
            {
                throw new ForbiddenException("Verification token has expired. Please register again.");
            }
            if(user.EmailVerified)
            {
                return ApiResponse<string>.SuccessResponse("Email already verified.", "Your email is already verified.");
            }
            user.EmailVerified = true;
            user.VerificationToken = null;
            user.VerificationTokenExpiry = null;
            user.Status = Models.Enum.AccountStatus.Active;
            await _context.SaveChangesAsync();
            return ApiResponse<string>.SuccessResponse("Email verified successfully.", "Your email has been verified.");
        }

        public async Task<ApiResponse<string>> ResendVerificationEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (user.EmailVerified)
                return ApiResponse<string>.SuccessResponse("Email is already verified.");

            user.VerificationToken = Guid.NewGuid();
            user.VerificationTokenExpiry = DateTime.UtcNow.AddDays(1);

            await _context.SaveChangesAsync();

            // 🔔 Send verification email logic here (through a service)
            _logger.LogInformation($"Resent verification token: {user.VerificationToken}");

            return ApiResponse<string>.SuccessResponse("Verification email resent.");
        }

    }
}

