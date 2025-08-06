using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
    {
        public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
        {
            public void Configure(EntityTypeBuilder<Problem> builder)
            {
                builder.ToTable("Problems");

                builder.HasKey(p => p.Id);

                builder.Property(p => p.Title).IsRequired().HasMaxLength(255);
                builder.Property(p => p.Description).IsRequired();

                // Map enum to string or int
                builder.Property(p => p.DifficultyLevel)
                    .HasColumnName("DifficultyLevel")
                    .HasConversion<int>() // or .HasConversion<string>()
                    .IsRequired();

                builder.HasMany(p => p.Submission)
                    .WithOne(s => s.Problem)
                    .HasForeignKey(s => s.ProblemId);

                builder.HasOne(p => p.LearningModule)
                    .WithMany(m => m.Problems)
                    .HasForeignKey(p => p.ModuleId);
            }
        }
    }

}
