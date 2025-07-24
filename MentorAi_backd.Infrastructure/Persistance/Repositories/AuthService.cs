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
using Org.BouncyCastle.Security;


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
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IGeneric<User> userRepository,
            IGeneric<StudentProfile> studentProfileRepo,
            IGeneric<ReviewerProfile> reviwerRepo,  
            IUnitOfWork unitOfWork,
            ITokenService tokenRepo, ILogger<AuthService> logger,IMapper mapper)
        {
            _reviwerRepo = reviwerRepo;
            _userRepo = userRepository;
            _studentProfileRepo = studentProfileRepo;
            _TokenRepo = tokenRepo;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
           
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
                
                _logger.LogInformation($"User {newUser.UserName} registered with role {newUser.UserRole}.");

                if(newUser.UserRole == UserEnum.Student)
        {
                    var studentProfile = new StudentProfile
                    {
                        User = newUser,

                    };
                    await _studentProfileRepo.AddAsync(studentProfile);
                    _logger.LogInformation($"Student profile created for user {newUser.Id}");
                }
                else if (newUser.UserRole == UserEnum.Reviewer)
                        {
                var reviewerProfile = new ReviewerProfile
                {
                    User = newUser,
                    Status = ReviewerStatus.Pending
                };
                newUser.ReviewerProfile = reviewerProfile;
                            await _reviwerRepo.AddAsync(reviewerProfile);
                            _logger.LogInformation($"Reviewer profile created for user {newUser.Id}");
                        }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation($"User {newUser.UserName} registered successfully with ID {newUser.Id}.");
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

            var accessToken = await _TokenRepo.GenerateAccessToken(user);
            var refreshToken = _TokenRepo.GenerateRefreshToken();
            
            user.RefreshToken = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            bool isProfileComplete = true;
            if (user.UserRole == UserEnum.Student)
            {
                isProfileComplete = await _studentProfileRepo.Query().AnyAsync(p => p.UserId == user.Id);
            }
            else if (user.UserRole == UserEnum.Reviewer)
            {
                isProfileComplete = await _reviwerRepo.Query().AnyAsync(p => p.UserId == user.Id);
            }

            await _userRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            var loginResponse = new LoginResponseDto
            {
                UserName = user.UserName,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserRole = user.UserRole.ToString(),
                ProfileImageUrl = user.ProfileImageUrl,
                isProfileComplete = isProfileComplete
            };
            return ApiResponse<LoginResponseDto>.SuccessResponse(
                loginResponse, "Login successful. Welcome back!");
        }

        public async Task<ApiResponse<string>> LogoutAsync(int userId, string refreshToken)
        {
            var result = await RevokeTokenAsync(userId, refreshToken);

            if (!result.Data)
            {
                throw new UnauthorizedException("Invalid or expired refresh token.");
            }

            return ApiResponse<string>.SuccessResponse("Logout successful.", "You have been logged out successfully.");
        }

        public async Task<ApiResponse<bool>> RevokeTokenAsync(int userId, string refreshToken)
        {
            var user = await _userRepo.Query().FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new UnauthorizedException("User not found.");

            // Ensure token is present and valid
            if (string.IsNullOrEmpty(user.RefreshToken) || !BCrypt.Net.BCrypt.Verify(refreshToken, user.RefreshToken))
                throw new UnauthorizedException("Invalid refresh token.");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            user.LastLogout = DateTime.UtcNow;

            await _userRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"User {user.UserName} logged out successfully.");

            return ApiResponse<bool>.SuccessResponse(true, "Token revoked successfully.");
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
            await _unitOfWork.SaveChangesAsync();
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
            await _unitOfWork.SaveChangesAsync();

           
            _logger.LogInformation($"Resent verification token: {user.VerificationToken}");

            return ApiResponse<string>.SuccessResponse("Verification email resent.");
        }

    }
}

