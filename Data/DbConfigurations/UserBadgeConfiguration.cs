using MentorAi_backd.Models.Entity;
using MentorAi_backd.Models.Entity.Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorAi_backd.Data.DbConfigurations
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
