using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool EmailVerified { get; set; }

    public Guid? VerificationToken { get; set; }

    public DateTime? VerificationTokenExpiry { get; set; }

    public Guid? PasswordResetToken { get; set; }

    public DateTime? PasswordResetTokenExpiry { get; set; }

    public int FailedLoginAttempts { get; set; }

    public DateTime? LastFailedLogin { get; set; }

    public DateTime? LastSuccessfulLogin { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public string Status { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public bool IsDeleted { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool ProfileCompleted { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? AcceptedTermsAt { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public string? OtpSecret { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual ICollection<Review> ReviewReviewers { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewStudents { get; set; } = new List<Review>();

    public virtual ReviewerProfile? ReviewerProfile { get; set; }

    public virtual StudentProfile? StudentProfile { get; set; }
}
