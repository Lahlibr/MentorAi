using MentorAi_backd.Domain.Entities;
using MentorAi_backd.Domain.Entities.Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    public class UserBadgeConfiguration : IEntityTypeConfiguration<UserBadge>
    {
        public void Configure(EntityTypeBuilder<UserBadge> builder)
        {
            builder.HasKey(ub => ub.Id);
            builder.Property(ub => ub.StudentProfileId).IsRequired(); 
            builder.Property(ub => ub.BadgeId).IsRequired();
            builder.Property(ub => ub.AwardedAt).IsRequired();

            // Relationships
            builder.HasOne(ub => ub.StudentProfile)
                .WithMany(sp => sp.UserBadges) 
                .HasForeignKey(ub => ub.StudentProfileId) 
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ub => ub.Badge)
                .WithMany(b => b.UserBadges)
                .HasForeignKey(ub => ub.BadgeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(ub => new { ub.StudentProfileId, ub.BadgeId }).IsUnique();
        }
    }
}
