using MentorAi_backd.Domain.Entities.Reviewer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    public class ReviewerAvailabilityConfiguration : IEntityTypeConfiguration<ReviewerAvailability>
    {
        public void Configure(EntityTypeBuilder<ReviewerAvailability> builder)
        {
           

            builder.HasOne(ra => ra.ReviewerProfile)
                   .WithMany(rp => rp.Availabilities)
                   .HasForeignKey(ra => ra.ReviewerProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
