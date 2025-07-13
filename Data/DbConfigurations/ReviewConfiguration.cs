using MentorAi_backd.Models.Entity.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorAi_backd.Data.DbConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.ProblemAttempt) 
                   .WithOne(pa => pa.Review)    
                   .HasForeignKey<Review>(r => r.ProblemAttemptId) 
                   .IsRequired(); 

            
            builder.HasOne(r => r.Student)
                   .WithMany(u => u.ReviewsAsStudent) 
                   .HasForeignKey(r => r.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Reviewer)
                   .WithMany(u => u.ReviewsAsReviewer) 
                   .HasForeignKey(r => r.ReviewerId)
                   .OnDelete(DeleteBehavior.Restrict); 

           
            builder.Property(r => r.Feedback)
                   .HasMaxLength(2000);

            builder.Property(r => r.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.Property(r => r.VideoConferenceLink)
                   .HasMaxLength(500);

        }
    }
}
