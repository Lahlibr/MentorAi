// MentorAi_backd.Infrastructure/Persistence.Data/MentorAiDbContext.cs
using MentorAi_backd.Domain.Entities.Main;
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
        public DbSet<Modules> Modules { get; set; } = default!; // Simplified DbSet declaration
        public DbSet<Roadmap> Roadmaps { get; set; } = default!;
        public DbSet<StudentRoadmapProgress> StudentRoadmapProgresses { get; set; } = default!;
        public DbSet<Problem> Problems { get; set; } = default!;
        public DbSet<ProblemAttempt> ProblemAttempts { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Certification> Certifications { get; set; } = default!;
        public DbSet<StudentCertification> StudentCertifications { get; set; } = default!;
        public DbSet<Badge> Badges { get; set; } = default!;
        public DbSet<UserBadge> UserBadges { get; set; } = default!;


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
            modelBuilder.Entity<Roadmap>().HasData(
                new Roadmap
                {
                    Id = 1,
                    Title = "Beginner C# Development",
                    Description = "Learn the fundamentals of C# programming.",
                    DifficultyLevel = "Beginner",
                    EstimatedCompletionHours = 40,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                }
            );

            // Seed Modules for the Roadmap (keeping here for now, can be moved to ModuleConfiguration)
            modelBuilder.Entity<Modules>().HasData(
                new Modules { Id = 1, RoadmapId = 1, Title = "Introduction to C#", Description = "Basic syntax and variables.", OrderInRoadmap = 1, CreatedAt = DateTime.UtcNow, LastUpdatedAt = DateTime.UtcNow },
                new Modules { Id = 2, RoadmapId = 1, Title = "Control Flow", Description = "If statements and loops.", OrderInRoadmap = 2, CreatedAt = DateTime.UtcNow, LastUpdatedAt = DateTime.UtcNow }
            );

            // Seed a Problem (keeping here for now, can be moved to ProblemConfiguration)
            modelBuilder.Entity<Problem>().HasData(
                new Problem
                {
                    Id = 1,
                    ModuleId = 1,
                    Title = "Sum of Two Numbers",
                    Description = "Write a function that takes two integers and returns their sum.",
                    DifficultyLevel = DifficultyLevelEnum.Easy,
                    InputFormat = "Two integers separated by a comma.",
                    OutputFormat = "Their sum.",
                    ExampleTestCasesJson = "[{\"input\":\"1,2\",\"output\":\"3\"},{\"input\":\"-5,10\",\"output\":\"5\"}]",
                    SolutionTemplate = "public class Solution { public int Sum(int a, int b) { /* Your code here */ return 0; } }",
                    ExpectedSolutionHash = "dummyhash1",
                    OrderInModule = 1,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                }
            );

            // Seed a Certification (keeping here for now, can be moved to CertificationConfiguration)
            modelBuilder.Entity<Certification>().HasData(
                new Certification
                {
                    Id = 1,
                    Title = "C# Fundamentals Certified",
                    Description = "Demonstrates basic proficiency in C#.",
                    Criteria = "Complete 'Beginner C# Development' roadmap.",
                    ImageUrl = "https://placehold.co/100x100/007bff/ffffff?text=C#Cert",
                    Issuer = "MentorAI",
                    IssuedDate = DateTime.UtcNow,
                    CertificateUrl = "https://mentorai.com/cert/csharp-1",
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                }
            );

            // Seed a Badge (keeping here for now, can be moved to BadgeConfiguration)
            modelBuilder.Entity<Badge>().HasData(
                new Badge
                {
                    Id = 1,
                    Name = "First Code",
                    Description = "Awarded for submitting your first coding solution.",
                    ImageUrl = "https://placehold.co/50x50/28a745/ffffff?text=Badge1",
                    Criteria = "Submit any problem solution.",
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
