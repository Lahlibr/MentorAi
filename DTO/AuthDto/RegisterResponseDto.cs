namespace MentorAi_backd.DTO.AuthDto
{
    public class RegisterResponseDto
    {
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public Guid? VerificationToken { get; set; }
    }
}

