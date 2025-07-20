// MentorAi_backd.Infrastructure/Persistence.Data/DbConfigurations/ReviewerProfileConfiguration.cs
using MentorAi_backd.Domain.Entities.UserEntity; // For ReviewerProfile, User
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System; // For DateTime.UtcNow

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    // CORRECTED: Implemented IEntityTypeConfiguration interface
    public class ReviewerProfileConfiguration : IEntityTypeConfiguration<ReviewerProfile>
    {
        public void Configure(EntityTypeBuilder<ReviewerProfile> builder)
        {
            builder.HasKey(rp => rp.UserId); // UserId is primary key for 1-to-1

            builder.Property(rp => rp.Id)
           
                    .ValueGeneratedNever(); // Id is not auto-generated, it comes from UserId

            builder.HasOne(rp => rp.User)
                    .WithOne(u => u.ReviewerProfile)
                    .HasForeignKey<ReviewerProfile>(rp => rp.UserId);

            // Property configurations
            builder.Property(rp => rp.Bio)
                    .HasMaxLength(1000);

            builder.Property(rp => rp.ExpertiseAreasJson)
                    .IsRequired(); // Ensure this is stored, even if empty array JSON

           
            
        }
    }
}
