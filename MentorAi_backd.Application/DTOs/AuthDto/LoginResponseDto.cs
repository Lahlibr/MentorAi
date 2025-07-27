namespace MentorAi_backd.Application.DTOs.AuthDto
{
    public class LoginResponseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserRole { get; set; }
        public string? ProfileImageUrl { get; set; }
        
        public bool isProfileComplete { get; set; } = false;
        public DateTime CreatedAt { get; set; }

    }
}

