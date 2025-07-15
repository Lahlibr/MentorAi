using MentorAi_backd.Domain.Entities.Student; // For StudentProfile
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BCrypt.Net;

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
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
            
        }
    }
}
