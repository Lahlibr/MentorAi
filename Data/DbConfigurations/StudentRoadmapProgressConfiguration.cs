using MentorAi_backd.Models.Entity.Student;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MentorAi_backd.Data.DbConfigurations
{
    public class StudentRoadmapProgressConfiguration : IEntityTypeConfiguration<StudentRoadmapProgress>
    {
        public void Configure(EntityTypeBuilder<StudentRoadmapProgress> builder)
        {
            builder.HasKey(srp => srp.Id);
            builder.HasOne(srp => srp.StudentProfile)
                   .WithMany(sp => sp.RoadmapProgresses)
                   .HasForeignKey(srp => srp.StudentProfileId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(srp => srp.Roadmap)
                   .WithMany() 
                   .HasForeignKey(srp => srp.RoadmapId)
                   .OnDelete(DeleteBehavior.Restrict); 

            
            builder.HasOne(srp => srp.CurrentModule)
                   .WithMany() 
                   .HasForeignKey(srp => srp.CurrentModuleId)
                   .IsRequired(false) 
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
