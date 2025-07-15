using System;
using System.Collections.Generic;
using MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;
using Microsoft.EntityFrameworkCore;

namespace MentorAi_backd.Infrastructure.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<ProblemAttempt> ProblemAttempts { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ReviewerProfile> ReviewerProfiles { get; set; }

    public virtual DbSet<Roadmap> Roadmaps { get; set; }

    public virtual DbSet<StudentCertification> StudentCertifications { get; set; }

    public virtual DbSet<StudentProfile> StudentProfiles { get; set; }

    public virtual DbSet<StudentRoadmapProgress> StudentRoadmapProgresses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBadge> UserBadges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ZAINSPC\\MSSQLSERVER01;Database=MentorAiDataBase;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasIndex(e => e.RoadmapId, "IX_Modules_RoadmapId");

            entity.HasOne(d => d.Roadmap).WithMany(p => p.Modules).HasForeignKey(d => d.RoadmapId);
        });

        modelBuilder.Entity<Problem>(entity =>
        {
            entity.HasIndex(e => e.ModuleId, "IX_Problems_ModuleId");

            entity.HasOne(d => d.Module).WithMany(p => p.Problems).HasForeignKey(d => d.ModuleId);
        });

        modelBuilder.Entity<ProblemAttempt>(entity =>
        {
            entity.HasIndex(e => e.ProblemId, "IX_ProblemAttempts_ProblemId");

            entity.HasIndex(e => e.StudentProfileId, "IX_ProblemAttempts_StudentProfileId");

            entity.HasOne(d => d.Problem).WithMany(p => p.ProblemAttempts).HasForeignKey(d => d.ProblemId);

            entity.HasOne(d => d.StudentProfile).WithMany(p => p.ProblemAttempts).HasForeignKey(d => d.StudentProfileId);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasIndex(e => e.ProblemAttemptId, "IX_Reviews_ProblemAttemptId").IsUnique();

            entity.HasIndex(e => e.ReviewerId, "IX_Reviews_ReviewerId");

            entity.HasIndex(e => e.StudentId, "IX_Reviews_StudentId");

            entity.Property(e => e.Feedback).HasMaxLength(2000);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.VideoConferenceLink).HasMaxLength(500);

            entity.HasOne(d => d.ProblemAttempt).WithOne(p => p.Review).HasForeignKey<Review>(d => d.ProblemAttemptId);

            entity.HasOne(d => d.Reviewer).WithMany(p => p.ReviewReviewers)
                .HasForeignKey(d => d.ReviewerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Student).WithMany(p => p.ReviewStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ReviewerProfile>(entity =>
        {
            entity.HasKey(e => e.UserId1);

            entity.Property(e => e.UserId1).ValueGeneratedNever();
            entity.Property(e => e.Availability).HasMaxLength(50);
            entity.Property(e => e.Bio).HasMaxLength(1000);

            entity.HasOne(d => d.UserId1Navigation).WithOne(p => p.ReviewerProfile).HasForeignKey<ReviewerProfile>(d => d.UserId1);
        });

        modelBuilder.Entity<StudentCertification>(entity =>
        {
            entity.HasIndex(e => e.CertificationId, "IX_StudentCertifications_CertificationId");

            entity.HasIndex(e => new { e.StudentProfileId, e.CertificationId }, "IX_StudentCertifications_StudentProfileId_CertificationId").IsUnique();

            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Certification).WithMany(p => p.StudentCertifications).HasForeignKey(d => d.CertificationId);

            entity.HasOne(d => d.StudentProfile).WithMany(p => p.StudentCertifications).HasForeignKey(d => d.StudentProfileId);
        });

        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasKey(e => e.UserId1);

            entity.Property(e => e.UserId1).ValueGeneratedNever();

            entity.HasOne(d => d.UserId1Navigation).WithOne(p => p.StudentProfile).HasForeignKey<StudentProfile>(d => d.UserId1);
        });

        modelBuilder.Entity<StudentRoadmapProgress>(entity =>
        {
            entity.HasIndex(e => e.CurrentModuleId, "IX_StudentRoadmapProgresses_CurrentModuleId");

            entity.HasIndex(e => e.RoadmapId, "IX_StudentRoadmapProgresses_RoadmapId");

            entity.HasIndex(e => e.StudentProfileId, "IX_StudentRoadmapProgresses_StudentProfileId");

            entity.HasOne(d => d.CurrentModule).WithMany(p => p.StudentRoadmapProgresses)
                .HasForeignKey(d => d.CurrentModuleId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Roadmap).WithMany(p => p.StudentRoadmapProgresses)
                .HasForeignKey(d => d.RoadmapId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.StudentProfile).WithMany(p => p.StudentRoadmapProgresses).HasForeignKey(d => d.StudentProfileId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RefreshToken).HasMaxLength(256);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserBadge>(entity =>
        {
            entity.HasIndex(e => e.BadgeId, "IX_UserBadges_BadgeId");

            entity.HasIndex(e => new { e.StudentProfileId, e.BadgeId }, "IX_UserBadges_StudentProfileId_BadgeId").IsUnique();

            entity.HasOne(d => d.Badge).WithMany(p => p.UserBadges).HasForeignKey(d => d.BadgeId);

            entity.HasOne(d => d.StudentProfile).WithMany(p => p.UserBadges).HasForeignKey(d => d.StudentProfileId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
