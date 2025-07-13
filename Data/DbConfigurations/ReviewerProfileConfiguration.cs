// MentorAi_backd/Data/DbConfigurations/ReviewerProfileConfiguration.cs
using MentorAi_backd.Models.Entity.UserEntity; // For ReviewerProfile, User
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System; // For DateTime.UtcNow

namespace MentorAi_backd.Data.DbConfigurations
{
    // CORRECTED: Implemented IEntityTypeConfiguration interface
    public class ReviewerProfileConfiguration : IEntityTypeConfiguration<ReviewerProfile>
    {
        public void Configure(EntityTypeBuilder<ReviewerProfile> builder)
        {
            builder.HasKey(rp => rp.UserId); // UserId is primary key for 1-to-1

            builder.Property(rp => rp.Id)
                    .HasColumnName("UserId")
                    .ValueGeneratedNever(); // Id is not auto-generated, it comes from UserId

            builder.HasOne(rp => rp.User)
                    .WithOne(u => u.ReviewerProfile)
                    .HasForeignKey<ReviewerProfile>(rp => rp.UserId);

            // Property configurations
            builder.Property(rp => rp.Bio)
                    .HasMaxLength(1000);

            builder.Property(rp => rp.ExpertiseAreasJson)
                    .IsRequired(); // Ensure this is stored, even if empty array JSON

            builder.Property(rp => rp.Availability)
                   .HasMaxLength(50); // Example max length for availability string

            // Seed data for Bob the Reviewer
            builder.HasData(
                new ReviewerProfile
                {
                    // CORRECTED: Id should match UserId for 1-to-1 PK/FK
                    Id = 2,
                    // CORRECTED: UserId should be 2 for Bob the reviewer
                    UserId = 2,
                    Bio = "Experienced .NET developer with a passion for clean code and mentorship.",
                    // CORRECTED: ExpertiseAreasJson must be a valid JSON array string
                    ExpertiseAreasJson = "[\"C#\", \".NET Core\", \"ASP.NET\", \"SQL Server\", \"Azure\"]",
                    YearsOfExperience = 5,
                    Availability = "Weekdays",
                    AverageRating = 4.5,
                    ReviewsCompleted = 50,
                    IsAvailableForReviews = true,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
