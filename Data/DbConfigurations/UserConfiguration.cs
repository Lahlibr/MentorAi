// MentorAi_backd/Data/DbConfigurations/UserConfiguration.cs
using MentorAi_backd.Models.Entity.Student; // For StudentProfile
using MentorAi_backd.Models.Entity.UserEntity; // For User, ReviewerProfile, Review
using MentorAi_backd.Models.Enum; // For UserEnum, AccountStatus
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BCrypt.Net; // For password hashing in seeding

namespace MentorAi_backd.Data.DbConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            builder.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            builder.HasIndex(u => u.Email)
                    .IsUnique();
            builder.Property(u => u.Password) 
                    .IsRequired()
                    .HasMaxLength(256);
            builder.Property(u => u.UserRole)
                    .IsRequired()
                    .HasConversion<string>();
            builder.Property(u => u.Status)
                .HasConversion<string>()
                .HasMaxLength(30);
            builder.Property(u => u.PhoneNumber).HasMaxLength(20);
            builder.Property(u => u.RefreshToken).HasMaxLength(256);

            // --- Relationships ---
            builder.HasOne(u => u.StudentProfile)
                .WithOne(sp => sp.User)
                .HasForeignKey<StudentProfile>(sp => sp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.ReviewerProfile)
                .WithOne(rp => rp.User)
                .HasForeignKey<ReviewerProfile>(rp => rp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.ReviewsAsReviewer)
                .WithOne(r => r.Reviewer)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ReviewsAsStudent)
                .WithOne(r => r.Student)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Seed initial data for User ---
            builder.HasData(
                new User
                {
                    Id = 1,
                    UserName = "alice_student",
                    Email = "alice@example.com",
                    UserRole = UserEnum.Student,
                    
                    
                    Password = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    EmailVerified = true,
                    Status = AccountStatus.Active,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    ProfileCompleted = true
                },
                new User
                {
                    Id = 2,
                    UserName = "bob_reviewer",
                    Email = "bob@example.com",
                    UserRole = UserEnum.Reviewer,
                  
                    // Marks = 0, // Removed from User entity
                    Password = BCrypt.Net.BCrypt.HashPassword("ReviewerPass!"),
                    EmailVerified = true,
                    Status = AccountStatus.Active,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    ProfileCompleted = true
                },
                new User
                {
                    Id = 3,
                    UserName = "charlie_admin",
                    Email = "charlie@example.com",
                    UserRole = UserEnum.Admin,
                   
                    // Marks = 0, // Removed from User entity
                    Password = BCrypt.Net.BCrypt.HashPassword("AdminPass!23"),
                    EmailVerified = true,
                    Status = AccountStatus.Active,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    ProfileCompleted = true
                }
            );
        }
    }
}
