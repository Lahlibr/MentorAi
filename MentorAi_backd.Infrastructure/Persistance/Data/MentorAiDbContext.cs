// MentorAi_backd.Infrastructure/Persistence.Data/MentorAiDbContext.cs
using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Domain.Entities.Reviewer;
using MentorAi_backd.Domain.Entities.Reviwer;
using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums; // For enums in seeding (if any remain here)
using Microsoft.EntityFrameworkCore;
using System.Reflection;
// Removed BCrypt.Net here as seeding is moved to configurations

namespace MentorAi_backd.Infrastructure.Persistance.Data
{
    public class MentorAiDbContext : DbContext
    {
        public MentorAiDbContext(DbContextOptions<MentorAiDbContext> options) : base(options) { }

        // --- DbSet Properties for all your Entities ---
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<StudentProfile> StudentProfiles { get; set; } = default!;
        public DbSet<ReviewerProfile> ReviewerProfiles { get; set; } = default!;
        public DbSet<ReviewerAvailability> ReviewerAvailabilities { get; set; } = default!;

        public DbSet<LearningModule> Modules { get; set; } = default!;
        public DbSet<Roadmap> Roadmaps { get; set; } = default!;
        public DbSet<StudentRoadmapProgress> StudentRoadmapProgresses { get; set; } = default!;
        public DbSet<Problem> Problems { get; set; } = default!;
        public DbSet<ProblemAttempt> ProblemAttempts { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Certification> Certifications { get; set; } = default!;
        public DbSet<StudentCertification> StudentCertifications { get; set; } = default!;
        public DbSet<Badge> Badges { get; set; } = default!;
        public DbSet<UserBadge> UserBadges { get; set; } = default!;
        public DbSet<RoadmapModule> RoadmapModules { get; set; }

        //Problems
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<TestCaseResultEntity> TestCaseResults { get; set; }


        // Automatically sets timestamps (CreatedAt and LastUpdatedAt) for entities inheriting from BaseEntity.
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                           (e.State == EntityState.Modified || e.State == EntityState.Added));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).LastUpdatedAt = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // --- Seed initial data for entities not having a dedicated config file yet, or for cross-entity seeding. ---
            // User, StudentProfile, ReviewerProfile, StudentCertification, UserBadge, StudentRoadmapProgress seeding
            // are now in their respective configuration files.

            // Seed a Roadmap (keeping here for now, can be moved to RoadmapConfiguration)
            

        }
    }
}
