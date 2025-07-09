using MentorAi_backd.Models.Enum;

namespace MentorAi_backd.Models.Entity
{
    public class User : BaseEntity
    {
        public int Id { get; set; }

       
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserEnum UserRole { get; set; } = UserEnum.Student;
        public int Age { get; set; }
        public int Marks { get; set; }

    
        public string PasswordHash { get; set; }
        public bool EmailVerified { get; set; } = false;

       
        public Guid? VerificationToken { get; set; }
        public DateTime? VerificationTokenExpiry { get; set; }
        public Guid? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }

     
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LastFailedLogin { get; set; }
        public DateTime? LastSuccessfulLogin { get; set; }
        public DateTime? LockoutEnd { get; set; }


        public AccountStatus Status { get; set; } = AccountStatus.PendingVerification;

    
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

     
        public bool IsDeleted { get; set; } = false;

    
        public string? ProfileImageUrl { get; set; }
        public bool ProfileCompleted { get; set; } = false;

    
        public string? PhoneNumber { get; set; }
        public DateTime? AcceptedTermsAt { get; set; }

       
        public bool TwoFactorEnabled { get; set; } = false;
        public string? OtpSecret { get; set; } 
    }
}
