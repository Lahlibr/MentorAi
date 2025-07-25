﻿// MentorAi_backd.Infrastructure/Persistence.Data/DbConfigurations/StudentProfileConfiguration.cs
using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Entities.UserEntity; // For User
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System; // For DateTime.UtcNow

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
    {
        public void Configure(EntityTypeBuilder<StudentProfile> builder)
        {
            builder.HasKey(sp => sp.UserId); // UserId is primary key for 1-to-1

           

            builder.HasOne(sp => sp.User)
                    .WithOne(u => u.StudentProfile)
                    .HasForeignKey<StudentProfile>(sp => sp.UserId);

            // --- Student-Specific Relationships ---
            builder.HasMany(sp => sp.RoadmapProgresses)
                    .WithOne(srp => srp.StudentProfile)
                    .HasForeignKey(srp => srp.StudentProfileId);

            builder.HasMany(sp => sp.ProblemAttempts)
                    .WithOne(pa => pa.StudentProfile)
                    .HasForeignKey(pa => pa.StudentProfileId);

            builder.HasMany(sp => sp.StudentCertifications)
                    .WithOne(sc => sc.StudentProfile)
                    .HasForeignKey(sc => sc.StudentProfileId);

            builder.HasMany(sp => sp.UserBadges)
                    .WithOne(ub => ub.StudentProfile)
                    .HasForeignKey(ub => ub.StudentProfileId);

            // Seed data for Alice's StudentProfile
            
        }
    }
}
