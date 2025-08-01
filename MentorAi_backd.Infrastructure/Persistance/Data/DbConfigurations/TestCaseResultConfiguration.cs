using MentorAi_backd.Domain.Entities.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    public class TestCaseResultConfiguration : IEntityTypeConfiguration<TestCaseResultEntity>
    {
        public void Configure(EntityTypeBuilder<TestCaseResultEntity> builder)
        {
            builder.HasOne(tcr => tcr.Submission)
                .WithMany(s => s.TestCaseResults)
                .HasForeignKey(tcr => tcr.SubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tcr => tcr.TestCase)
                .WithMany()
                .HasForeignKey(tcr => tcr.TestCaseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
