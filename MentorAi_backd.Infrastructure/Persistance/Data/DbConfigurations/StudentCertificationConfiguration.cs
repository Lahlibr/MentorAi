using MentorAi_backd.Domain.Entities.Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    public class StudentCertificationConfiguration : IEntityTypeConfiguration<StudentCertification>
    {
        public void Configure(EntityTypeBuilder<StudentCertification> builder)
        {
            builder.HasKey(sc => sc.Id);

            
            builder.HasOne(sc => sc.StudentProfile)
                   .WithMany(sp => sp.StudentCertifications)
                   .HasForeignKey(sc => sc.StudentProfileId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(sc => sc.Certification)
                   .WithMany(c => c.StudentCertifications)
                   .HasForeignKey(sc => sc.CertificationId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasIndex(sc => new { sc.StudentProfileId, sc.CertificationId }).IsUnique();


            builder.Property(sc => sc.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);

        }
    }
}
