using MentorAi_backd.Domain.Enums;

namespace MentorAi_backd.Application.DTOs.BaseDto
{
    public abstract class BaseUserProfileDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserEnum UserRole { get; set; } 
        public bool EmailVerified { get; set; }
        public AccountStatus Status { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
