using AutoMapper;
using MentorAi_backd.Application.DTOs.AuthDto;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MentorAi_backd.Services.Interfaces;
using MentorAi_backd.Application.Interfaces;
using Microsoft.Extensions.Logging;


namespace MentorAi_backd.Repositories.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IGeneric<User> _userRepo;
        private readonly IGeneric<StudentProfile> _studentProfileRepo;
        private readonly IGeneric<ReviewerProfile> _reviwerRepo;
        private readonly ITokenService _TokenRepo;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;

        public AuthService(IGeneric<User> userRepository,
            IGeneric<StudentProfile> studentProfileRepo,
            IGeneric<ReviewerProfile> reviwerRepo,  
            ITokenService tokenRepo, ILogger<AuthService> logger,IMapper mapper)
        {
            _reviwerRepo = reviwerRepo;
            _userRepo = userRepository;
            _studentProfileRepo = studentProfileRepo;
            _TokenRepo = tokenRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userRepo.Query().AnyAsync(u => u.UserName == registerDto.UserName))
            {
                throw new ConflictException("User with this Username already exists");
            }
            if (await _userRepo.Query().AnyAsync(u => u.Email == registerDto.Email))
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
                
            };

           
                await _userRepo.AddAsync(newUser);
                if (newUser.UserRole == UserEnum.Student)
                {
                    var studentProfile = new StudentProfile
                    {
                        UserId = newUser.Id,
                        Age = 0,
                        AssessmentScore = 0,
                        CurrentLearningGoal = "Not set",
                        PreferredLearningStyle = "Not set"
                    };
                    await _studentProfileRepo.AddAsync(studentProfile);
                }else if (newUser.UserRole == UserEnum.Reviewer)
                {
                    var reviewerProfile = new ReviewerProfile
                    {
                        UserId = newUser.Id,
                        Bio = "Not set",
                        YearsOfExperience = 0,
                        Availability = "Full-time",
                        ExpertiseAreasJson = "[]",
                        AverageRating = 0.0,
                        ReviewsCompleted = 0,
                        IsAvailableForReviews = true
                    };
                    await _reviwerRepo.AddAsync(reviewerProfile);
                }
                _logger.LogInformation($"User {newUser.UserName} registered with role {newUser.UserRole}.");

                var response = _mapper.Map<RegisterResponseDto>(newUser);
                response.Message = "Registration successful. Please check your email for verification link.";

                return ApiResponse<RegisterResponseDto>.SuccessResponse(response, response.Message);
        }

        public async Task<ApiResponse<LoginResponseDto>>LoginAsync(LoginDto loginDto){
            var user = await _userRepo.Query().FirstOrDefaultAsync(u => u.Email == loginDto.Email && !u.IsDeleted);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                if (user != null)
                {
                    user.FailedLoginAttempts++;
                    user.LastFailedLogin = DateTime.UtcNow;
                    if (user.FailedLoginAttempts >= 5 && user.LockoutEnd == null)
                    {
                        user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
                        user.Status = AccountStatus.LockedOut;
                        _logger.LogWarning($"Account '{user.Email}' locked due to failed login attempts.");
                    }await _userRepo.UpdateAsync(user);
                }
          
                
               
                throw new UnauthorizedException("Invalid credentials."); }
            if (user.Status == AccountStatus.LockedOut)
            {
                if (user.LockoutEnd > DateTime.UtcNow)
                {
                    throw new ForbiddenException($"Account locked out. Try again after {user.LockoutEnd.Value.Subtract(DateTime.UtcNow).Minutes} minutes.");
                }
                else
                {
                    user.Status = AccountStatus.Active;
                    user.FailedLoginAttempts = 0;
                    user.LockoutEnd = null;
                    _logger.LogInformation($"Account '{user.Email}' unlocked after lockout period.");
                }
            }
            //if (user.Status == Models.Enum.AccountStatus.PendingVerification)
            //{
            //    throw new ForbiddenException("Account not verified. Please check your email for verification link.");
            //}
            if(user.Status == AccountStatus.Blocked)
            {
                throw new ForbiddenException("Account is blocked. Contact support for assistance.");
            }
            user.FailedLoginAttempts = 0;
            user.LastSuccessfulLogin = DateTime.UtcNow;

            var accessToken = _TokenRepo.GenerateAccessToken(user);
            var refreshToken = _TokenRepo.GenerateRefreshToken();
            
            user.RefreshToken = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
            await _userRepo.UpdateAsync(user);
            
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

        public async Task<ApiResponse<string>> LogoutAsync(int userId)
        {
            var user = await _userRepo.Query().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid refresh token.");
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userRepo.UpdateAsync(user);
            _logger.LogInformation($"User {user.UserName} logged out successfully.");
            return ApiResponse<string>.SuccessResponse("Logout successful.", "You have been logged out successfully.");
        }
        public async Task<ApiResponse<string>> VerifyEmailAsync(Guid token)
        {
            var user = await _userRepo.Query().FirstOrDefaultAsync(u => u.VerificationToken == token);

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
            user.Status = AccountStatus.Active;
            await _userRepo.UpdateAsync(user);
            return ApiResponse<string>.SuccessResponse("Email verified successfully.", "Your email has been verified.");
        }

        public async Task<ApiResponse<string>> ResendVerificationEmailAsync(string email)
        {
            var user = await _userRepo.Query().FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (user.EmailVerified)
                return ApiResponse<string>.SuccessResponse("Email is already verified.");

            user.VerificationToken = Guid.NewGuid();
            user.VerificationTokenExpiry = DateTime.UtcNow.AddDays(1);

            await _userRepo.UpdateAsync(user);

            // 🔔 Send verification email logic here (through a service)
            _logger.LogInformation($"Resent verification token: {user.VerificationToken}");

            return ApiResponse<string>.SuccessResponse("Verification email resent.");
        }

    }
}

